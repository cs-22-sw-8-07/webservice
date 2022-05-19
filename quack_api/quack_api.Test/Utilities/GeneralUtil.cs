using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace quack_api.Test.Utilities
{
    public class GeneralUtil
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
               .AddJsonFile("appsettings.test.json")
               .Build();
            return config;
        }
        public static string LoadJson(string asset)
        {
            using (StreamReader r = new(asset))
            {
                return r.ReadToEnd();
            }
        }
    }
}
