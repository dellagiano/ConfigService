using System;
namespace ConfigService.Service
{
    public interface IDataInterface
    {
        string ReadConfig(string key, string countrycode, string mode, string service);

        IEnumerable<KeyValuePair<string,string>> ReadConfigSection(string key, string countrycode, string mode, string service);
    }
}

