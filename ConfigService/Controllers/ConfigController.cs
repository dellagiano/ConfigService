using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConfigService.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ConfigService.Controllers
{
    [Route("api/[controller]")]
    public class ConfigController : Controller
    {
        public IConfiguration _configuration { get; }

        public ConfigController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        //// GET: api/values
        //[HttpGet]
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        //api/config/ng/dev/financier_service/stanbic:appkey
        // GET api/values/5
        [HttpGet("{countrycode}/{mode}/{service_name}/{key}")]
        public IActionResult Get(string countrycode, string mode, string service_name, string key)
        {
            string api_key = Request.Headers["api_key"];

            string correct_api_key = _configuration.GetValue<string>("api_key");

            if (api_key != correct_api_key)
                return Unauthorized(false);

            IDataInterface _IData = new DataFileService();

            if (key.EndsWith(":"))
                return Ok(_IData.ReadConfigSection(key, countrycode, mode, service_name).AsEnumerable());
            else
                return Ok(_IData.ReadConfig(key, countrycode, mode, service_name));
        }

        //// POST api/values
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}

