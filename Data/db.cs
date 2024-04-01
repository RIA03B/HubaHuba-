using Microsoft.Extensions.Configuration;
using System.IO;

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
