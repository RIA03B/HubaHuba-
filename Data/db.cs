using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace HBPOS.Data
{
    public class db
    {
        public string getConString()
        {
            var configuation = GetConfiguration();
            string con = configuation.GetSection("ConnectionStrings").GetSection("DefaultConnection").Value;
            return con;
        }


        public IConfigurationRoot GetConfiguration()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            return builder.Build();
        }
    }
}
