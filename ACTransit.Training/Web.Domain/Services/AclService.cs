using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Xml.Linq;
using ACTransit.Entities.ActiveDirectory;
using ACTransit.Framework.Caching;
using ACTransit.Framework.Extensions;
using ACTransit.Training.Web.Business.ActiveDirectory;
using ACTransit.Training.Web.Business.Employee;
using ACTransit.Training.Web.Domain.Infrastructure;
using Cache = ACTransit.Framework.Caching.Cache;

namespace ACTransit.Training.Web.Domain.Services
{
    public class AclService:IDisposable
    {
        private static XElement _root;
        private readonly string _path="ACL.xml";
        private FileSystemWatcher fileMon;

        private readonly object _dynamicAccessLock=new object();
        private static readonly ManualResetEvent _dynamicWriteLock = new ManualResetEvent(true);        
        private static readonly CountdownEvent _numberOfReadsCountDownEvent=new CountdownEvent(1);
        private static readonly object _instanceLock = new object();        

        private static AclService _instance;
        public static AclService Create()
        {
            return Create(Common.AclPath);
        }
        public static AclService Create(string aclFilePath)
        {
            lock (_instanceLock)
            {
                if (_instance == null)
                    _instance = new AclService(aclFilePath);                
            }
            return _instance;
        }

        private AclService(string path)
        {
            _path = path;
            var file = new FileInfo(_path);
            fileMon=new FileSystemWatcher(file.DirectoryName,file.Name);
            fileMon.Changed += fileMon_Changed;
            fileMon.EnableRaisingEvents = true;
        }

        private void fileMon_Changed(object sender, FileSystemEventArgs e)
        {
            CacheAcl.ClearAll();
            _root = null;
        }


        public bool HasAccess(string token, string user)
        {
            var users = GetUserItems(token, user);
            if (users != null && users.Any())
            {
                if (users.Any(m => !m.HasAccess))
                    return false;
                return true;
            }

            var groups = GetGroups(token);
            if (groups != null && groups.Length > 0)
                using (var service = new ActiveDirectoryService(Settings.ActiveDirectoryUrl,
                    Settings.ActiveDirectoryUser, Settings.ActiveDirectoryPwd))
                {
                    var usergroups = service.GetGroupsOfUser(user, true);
                    var groupsInCommon = groups.Where(m => usergroups.Any(u => string.Equals(u.SamAccountName, m.Name, StringComparison.OrdinalIgnoreCase))).ToArray();
                    if (groupsInCommon.Any())
                    {
                        if (groupsInCommon.Any(m => !m.HasAccess))
                            return false;
                        return true;
                    }
                }

            var jobTitles = GetJobTitles(token);
            if (jobTitles != null && jobTitles.Length > 0)
                using (var service = new EmployeeService())
                {
                    var employees = service.GetEmployees(new string[] { user }).Select(m=>new {m.JobTitle }).ToList();
                    var groupsInCommon = jobTitles.Where(m => employees.Any(u => string.Equals(u.JobTitle, m.Name, StringComparison.OrdinalIgnoreCase))).ToArray();
                    if (groupsInCommon.Any())
                    {
                        if (groupsInCommon.Any(m => !m.HasAccess))
                            return false;
                        return true;
                    }
                }

            return false;

        }

        public bool HasDynamicAccess<T>(T obj, string userName)
        {
            userName = PrepareUser(userName);
            Group[] groupsOfUser = null;
            var typeName = typeof(T).Name;
            var tokens = GetDynamic(typeName);

            try
            {
                lock (_dynamicAccessLock)
                {
                    _dynamicWriteLock.WaitOne();
                    _numberOfReadsCountDownEvent.AddCount();
                }


                if (tokens == null || tokens.Length == 0) // nothing found for this in ACL. so we return true.
                    return true;
                var key = "DynamicAccess_" + typeName + "_";
                key += typeof (T).GetProperty(tokens[0].Name).GetValue(obj).ToString();
                var result = (bool?) CacheAcl.GetCache(key);
                if (result.HasValue)
                    return result.Value;

                foreach (var token in tokens)
                {
                    bool isAll = false;
                    var propName = token.Name;
                    var propValue = token.Value;
                    if (string.IsNullOrWhiteSpace(propValue) || propValue.Trim() == "*")
                        isAll = true;
                    
                    if (isAll || typeof (T).GetProperty(propName).GetValue(obj).ToString() == propValue)
                    {
                    
                        var user = token.Users.FirstOrDefault(m => m.Name == userName);
                        if (user != null)
                            result = user.HasAccess;
                        if (result.HasValue)
                            break;
                        
                        if (groupsOfUser == null)
                        {
                            using (
                                var service = new ActiveDirectoryService(Settings.ActiveDirectoryUrl,
                                    Settings.ActiveDirectoryUser, Settings.ActiveDirectoryPwd))
                            {
                                
                                groupsOfUser = service.GetGroupsOfUser(userName, true);
                            }
                            if (groupsOfUser == null)
                                groupsOfUser = new Group[0];
                        }
                        
                        var groupsInCommon =
                            token.Groups.Where(
                                m =>
                                    groupsOfUser.Any(
                                        u =>
                                            string.Equals(u.SamAccountName, m.Name,
                                                StringComparison.OrdinalIgnoreCase))).ToArray();
                        if (groupsInCommon.Any())
                        {
                            if (groupsInCommon.Any(m => !m.HasAccess))
                                result = false;
                            else
                                result = true;
                            break;
                        }
                    }
                }

                

                if (!result.HasValue)
                    result = false;
                CacheAcl.AddCache(key, result.Value);
                return result.Value;
            }
            finally
            {
                _numberOfReadsCountDownEvent.Signal();
            }
        }

        public string[] GetAuthorizedUsersTo(string token, bool includeSilence=true)
        {

            var adIds = new List<string>();
            var users = GetUsers(token);

            var groups = GetGroups(token);
            using (var service = new ActiveDirectoryService(Settings.ActiveDirectoryUrl,
                              Settings.ActiveDirectoryUser, Settings.ActiveDirectoryPwd))
            {
                foreach (var group in groups)
                {
                    if (group.HasAccess)
                    {
                        if (includeSilence || !group.Silence)
                        {
                            var temp = service.GetUsersInGroup(group.Name,true);
                            if (temp != null)
                                adIds.AddRange(temp.Select(m => m.SamAccountName).ToArray());
                        }
                    }
                }

            }
            adIds.AddRange(users.Where(m => m.HasAccess && (includeSilence || !m.Silence)).Select(m => m.Name).ToArray());

            using (var service = new EmployeeService())
            {
                var result = service.GetEmployees(adIds.ToArray()).Select(m => new { m.FirstName, m.LastName}).ToList();                
                return result.Select(m => string.Format("{0} {1}", m.FirstName.NullableTrim(), m.LastName.NullableTrim())).ToArray();
            }
        }

        private Item[] GetGroups(string token)
        {
            var tokens = Root.Elements("tokens");
            return
                GetGroups(tokens.Elements("token").FirstOrDefault(m => m.Attribute("name").Value == token));
        }

        private Item[] GetUsers(string token)
        {
            var tokens = Root.Elements("tokens");
            return GetUsers(tokens.Elements("token").FirstOrDefault(m => m.Attribute("name").Value == token));
        }

        private Item[] GetGroups(XElement token)
        {
            return token==null? new Item[0]: token
                    .Elements("groups")
                    .Elements("group")
                    .Select(m => new Item
                    {
                        Name = m.Value,
                        HasAccess =
                            (m.Attribute("allow").Value == "1" ||
                             string.Equals(m.Attribute("allow").Value, "true", StringComparison.OrdinalIgnoreCase)),
                        Silence = m.Attribute("silence") != null && ((m.Attribute("silence").Value == "1" || string.Equals(m.Attribute("silence").Value, "true", StringComparison.OrdinalIgnoreCase))),

                    }).ToArray();
        }

        private Item[] GetJobTitles(string token)
        {
            var tokens = Root.Elements("tokens");
            return GetJobTitles(tokens.Elements("token").FirstOrDefault(m => m.Attribute("name").Value == token));
        }

        private Item[] GetJobTitles(XElement token)
    {
        return token == null ? new Item[0] : token
                .Elements("jobTitles")
                .Elements("jobTitle")
                .Select(m => new Item
                {
                    Name = m.Value,
                    HasAccess =
                        (m.Attribute("allow").Value == "1" ||
                         string.Equals(m.Attribute("allow").Value, "true", StringComparison.OrdinalIgnoreCase)),
                    Silence = m.Attribute("silence") != null && ((m.Attribute("silence").Value == "1" || string.Equals(m.Attribute("silence").Value, "true", StringComparison.OrdinalIgnoreCase))),

                }).ToArray();
    }

    private Item[] GetUsers(XElement token)
        {
            return
                token                    
                    .Elements("users")
                    .Elements("user")
                    .Select(m => new Item
                    {
                        Name = m.Value,
                        HasAccess = (m.Attribute("allow").Value == "1" || string.Equals(m.Attribute("allow").Value, "true", StringComparison.OrdinalIgnoreCase)),
                        Silence = m.Attribute("silence") != null && ((m.Attribute("silence").Value == "1" || string.Equals(m.Attribute("silence").Value, "true", StringComparison.OrdinalIgnoreCase))),
                    }).ToArray();
        }    

        private Item[] GetUserItems(string token,string user)
        {
            var tokens = Root.Elements("tokens");
            return
                tokens.Elements("token")
                    .Where(m => m.Attribute("name").Value == token)
                    .Elements("users")
                    .Elements("user")
                    .Where(m=>string.Equals(m.Value,user,StringComparison.OrdinalIgnoreCase))
                    .Select(m => new Item {
                        Name = m.Value, 
                        HasAccess =(m.Attribute("allow").Value == "1" || string.Equals(m.Attribute("allow").Value,"true",StringComparison.OrdinalIgnoreCase)),
                        Silence = m.Attribute("silence") != null && ((m.Attribute("silence").Value == "1" || string.Equals(m.Attribute("silence").Value, "true", StringComparison.OrdinalIgnoreCase))),
                    }).ToArray();
        }

        private Token[] GetDynamic(string type)
        {
            var key = "ACL_GetDynamic_" + type;
            var result = (Token[]) CacheAcl.GetCache(key);
            if (result == null)
            {
                try
                {               
                    lock (_dynamicAccessLock)
                    {
                        _dynamicWriteLock.WaitOne();
                        _dynamicWriteLock.Reset();
                        _numberOfReadsCountDownEvent.Signal();
                        _numberOfReadsCountDownEvent.Wait();
                    }
                    result = (Token[])CacheAcl.GetCache(key);
                    if (result == null)
                    {
                        result = GetDynamicTokensFromFile(type);
                        CacheAcl.AddCache(key, result);
                    }
                }
                finally
                {
                    _numberOfReadsCountDownEvent.Reset(1);                    
                    _dynamicWriteLock.Set();                    
                }
            }
            return result;

        }

        private Token[] GetDynamicTokensFromFile(string type)
        {
            var result = new List<Token>();
            var dynamic = Root.Elements("dynamic");
            var types = dynamic.Elements("types");
            var tokens =
                types.Elements("type").Where(m => m.Attribute("name").Value == type).Elements("token");
            foreach (var t in tokens)
            {
                var name = t.Attribute("name").Value;
                var value = t.Attribute("value").Value;
                var users = GetUsers(t);
                var groups = GetGroups(t);
                var token = new Token { Name = name, Value = value, Users = users, Groups = groups };
                result.Add(token);
            }

            return result.ToArray();       
        }


        private void Load()
        {            
            _root = XElement.Load(_path);
        }
        
        private XElement Root
        {
            get
            {                
                if (_root == null)
                    Load();
                return _root;
            }
        }

        public string PrepareUser(string user)
        {
            if (!string.IsNullOrWhiteSpace(user))
            {
                if (user.Contains('\\'))
                    user = user.Split('\\')[1];
                if (user.Contains("@"))
                    user = user.Split('@')[0];
            }
            return user;
        }

        private class Token
        {
            public string Name { get; set; }
            public string Value { get; set; }
            public Item[] Groups { get; set; }
            public Item[] Users { get; set; }
        }

        private class Item
        {
            public string Name { get; set; }
            public bool HasAccess { get; set; }
            public bool Silence { get; set; }
        }

        private static ICache _cacheAcl;
        public static ICache CacheAcl
        {
            get
            {
                if (_cacheAcl == null)
                    _cacheAcl = new Cache("ACTransit.Training.Web.Domain_ACL");
                return _cacheAcl;
            }
        }

        public void Dispose()
        {
            if (fileMon!=null)
                fileMon.Dispose();            
            //todo
        }
    }
}
