using System;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace ConfigService.Service
{
    public class DataFileService : IDataInterface
    {
        private readonly string _serverPath = ServerPath.RootPath();


        private IConfigurationRoot ReadConfigSetting(string countrycode , string mode , string service )
        {
            string dataFolderPath = Path.Combine(_serverPath, "Data/");
            string jsonConfigFilePath = $"{dataFolderPath}/{countrycode}/{mode}/{service}_config.json";

            var configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               //.AddJsonFile("appsettings.json")
               .AddJsonFile(jsonConfigFilePath)
               .Build();

            return configuration;
        }

        public IEnumerable<KeyValuePair<string,string>> ReadConfigSection(string key, string countrycode, string mode, string service)
        {
            var configuration = ReadConfigSetting(countrycode, mode, service);
             
            return configuration.GetSection(key).AsEnumerable(); 
        }

        public string ReadConfig(string key, string countrycode, string mode, string service)
        {
            var configuration = ReadConfigSetting(countrycode, mode, service);

            return configuration.GetValue<string>(key);
        }
    }
}

