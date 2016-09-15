using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.IO;
using Mono.Options;
using SQlExecute.Models;


namespace SQlExecute
{
    class Program
    {     
        static void Main(string[] args)
        {
            var options = new Option(args);
            var sql = new Sql(options);
            sql.Execute();
        }
    }
}
