
using System.IO;
using Microsoft.Extensions.Configuration;

namespace ECommerceAPI.Persistence
{
    public static class Configuration
    {
        public static string? GetConnectionString
        {
            get
            {
                ConfigurationManager configurationManager = new();
                configurationManager.SetBasePath(Path.Combine(Directory.GetCurrentDirectory(),
                    "../../Presentation/ECommerceAPI.API"));
                    configurationManager.AddJsonFile("appsettings.json");

                return configurationManager.GetConnectionString("Mssql");
            }
        }
    }
}