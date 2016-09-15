using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Mono.Options;
using SQlExecute.Models;

namespace SQlExecute
{
    public class Option
    {
        public string DataDirectory { get; private set; }
        public string ScriptDirectory { get; private set; }
        public bool ShowHelp { get; private set; }
        public bool TestMode { get; private set; }
        public string FilePath { get; private set; }
        public string CurrentDirectory { get; private set; }


        public Option(string[] args)
        {
            //args = new string[]
            //{
            //    @"-s",
            //    @"C:\Projects\ACTransit.Projects\GitHub_Trunk\PublishScripts",
            //    @"-d",
            //    @"C:\Projects\ACTransit.Projects\GitHub_Trunk\ACTransit.Training\Web\App_Data\",
            //    @"-t",
            //    @"true",
            //    @"CleanupDB.sql"
            //};

            //args = new string[]
            //{
            //    @"-t",
            //    @"CleanupDB.sql"
            //};

            //args = new string[]
            //{
            //    @"--testmode=false",
            //    @"--scriptdir=C:\Projects\ACTransit.Projects\GitHub_Trunk\PublishScripts",
            //    @"--datadir=C:\Projects\ACTransit.Projects\GitHub_Trunk\ACTransit.Training\Web\App_Data\",
            //    @"--currentdir=C:\Projects\ACTransit.Projects\GitHub_Trunk\ACTransit.Training\Web\App_Data",
            //    @"C:\Projects\ACTransit.Projects\GitHub_Trunk\PublishScripts\ReplaceText.sql",
            //};

            ShowHelp = args.Length == 0;
            TestMode = false;
            FilePath = "";
            CurrentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var extra = new List<string>();

            var opt = new OptionSet()
            {
                {"t|testmode=", "Just return the name of the files/databases without executing it", (p) => TestMode = Convert.ToBoolean(p) },
                {"s|scriptdir=", "Directory where PublishScripts is located", (p) => ScriptDirectory = p },
                {"d|datadir=", "Directory where App_Data (or similar) is located", (p) => DataDirectory = p },
                {"c|currentdir=", "Directory where current directory is located", (p) => CurrentDirectory = p },
                {"h|?|help", "Prints out the options.", (v) => ShowHelp = true },
            };
            try
            {
                extra = opt.Parse(args);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine("Try 'sqlexecute --help' for more information.");
                Environment.Exit(1);
            }

            if (ShowHelp)
                WriteHelp(opt);

            if (string.IsNullOrEmpty(ScriptDirectory))
                ScriptDirectory = GetDirectory("ScriptDirectory") ?? CurrentDirectory;
            if (string.IsNullOrEmpty(DataDirectory))
                DataDirectory = GetDirectory("DataDirectory") ?? CurrentDirectory;

            if (extra.Count > 0)
            {
                FilePath = Path.GetFullPath(Path.Combine(ScriptDirectory, extra[0]));
            }
        }

        private string GetDirectory(string name)
        {
            var dataDirectoryappSetting = ConfigurationManager.AppSettings[name];
            var baseDir = AppDomain.CurrentDomain.BaseDirectory;
            var path = Path.Combine(baseDir, dataDirectoryappSetting);
            var result = Path.GetFullPath(path);
            AppDomain.CurrentDomain.SetData(name, result);
            return Path.GetFullPath(path);
        }

        private void WriteHelp(OptionSet opt)
        {
            Console.WriteLine();
            Console.WriteLine("sqlexecute [OPTIONS]+ sqlfile");
            Console.WriteLine();
            Console.WriteLine("Options:");
            opt.WriteOptionDescriptions(Console.Out);
            Console.WriteLine("Example:");
            Console.WriteLine("  sqlexecute -t --scriptdir=c:\\test test.sql");
            Console.WriteLine("  sqlexecute c:\\test\\test.sql");
            Console.WriteLine();
            Environment.Exit(6);
        }
    }

    public class Sql
    {
        private readonly Option option;

        public Sql(Option option)
        {
            this.option = option;
        }

        public void Execute()
        {            
            var tokens = GetTokens();
            try
            {
                if (option.TestMode)
                {
                    var errorStr = new StringBuilder();
                    if (!File.Exists(option.FilePath))
                        errorStr.AppendLine($"FilePath: '{option.FilePath}' not found!");
                    if (!Directory.Exists(option.DataDirectory))
                        errorStr.AppendLine($"DataDirectory: '{option.DataDirectory}' not found!");
                    if (!Directory.Exists(option.ScriptDirectory))
                        errorStr.AppendLine($"ScriptDirectory: '{option.ScriptDirectory}' not found!");
                    Console.WriteLine();
                    Console.WriteLine("SqlExecute test mode.");
                    Console.WriteLine();
                    Console.WriteLine($"FilePath: {option.FilePath}");
                    Console.WriteLine($"DataDirectory: {option.DataDirectory}");
                    Console.WriteLine($"ScriptDirectory: {option.ScriptDirectory}");
                    Console.WriteLine("Tokens:");
                    foreach (var token in tokens)
                    {
                        Console.WriteLine($"'{token.TokenName}' will be replaced with '{token.TokenValue}'");
                        if (!File.Exists(token.TokenValue))
                            errorStr.AppendLine($"File for TokenValue: '{token.TokenValue}' not found!");
                    }
                    if (errorStr.Length > 0)
                    {
                        Console.WriteLine("Errors:");
                        Console.WriteLine(errorStr.ToString());
                    }
                    Console.WriteLine();
                    Environment.Exit(2);
                }

                UpdateDataDirectory();
                Directory.SetCurrentDirectory(option.CurrentDirectory);
                var cmdStr = File.ReadAllText(option.FilePath);
                cmdStr = PrepareCmd(tokens, cmdStr);
                ExecuteNonQuery(cmdStr);
                Environment.Exit(0);
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(3);

            }
            catch (Exception e)
            {
                Console.WriteLine("Error:");
                Console.WriteLine(e.Message);
                while (e.InnerException != null)
                {
                    Console.WriteLine(e.InnerException.Message);
                    e = e.InnerException;
                }
                Environment.Exit(4);
            }
        }

        private void UpdateDataDirectory(bool useAssemblyLocation = false)
        {
            var path = option.DataDirectory;
            if (useAssemblyLocation)
            {
                var executable = System.Reflection.Assembly.GetExecutingAssembly().Location;
                path = (Path.GetDirectoryName(executable));
            }
            AppDomain.CurrentDomain.SetData("DataDirectory", path);
        }

        private string GetConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["Default"].ConnectionString;
            //return Regex.Replace(
            //        ConfigurationManager.ConnectionStrings["Default"].ConnectionString.Replace("|DataDirectory|", option.DataDirectory), 
            //        @"AttachDbFilename=""(.*?)""",
            //        @"AttachDbFilename=[$1]");
        }

        private void ExecuteNonQuery(string cmdStr)
        {
            using (var cnn = new SqlConnection(GetConnectionString()))
            {
                cnn.Open();
                using (var cmd = new SqlCommand(cmdStr, cnn))
                {
                    var result = cmd.ExecuteNonQuery();
                    Console.WriteLine($"Result: {result}");
                }
            }
        }
        private string PrepareCmd(IEnumerable<Token> tokens, string cmdStr)
        {
            var result = new StringBuilder(cmdStr);
            result = tokens.Aggregate(result, (current, token) => current.Replace(token.TokenName, token.TokenValue));
            return result.ToString();
        }

        private List<Token> GetTokens()
        {
            var result = new List<Token>();
            var tokens = ((TokenConfigSection)ConfigurationManager.GetSection("TokenConfigSection")).Tokens;
            foreach (TokenConfig tokenConf in tokens)
            {
                var value = tokenConf.TokenValue
                    .Replace("|DataDirectory|", option.DataDirectory)
                    .Replace("|ScriptDirectory|",  option.ScriptDirectory );
                result.Add(new Token(tokenConf.TokenName, value));
            }
            return result;
        }
    }
}
