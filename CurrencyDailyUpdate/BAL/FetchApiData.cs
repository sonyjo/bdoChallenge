using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using CurrencyDailyUpdate.Model;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Data;
using System.Linq;

namespace CurrencyDailyUpdate.BAL
{
    public class FetchApiData
    {
        private readonly IConfiguration _configuration;
        private static readonly HttpClient client = new HttpClient();
        public FetchApiData(IConfiguration configuration)
        {
            _configuration = configuration;

        }

        // This method is used for consuming data from Fixer API
        public async Task<FixerApiResponse> MakeApiCall()
        {
            try
            {
                var apikey = _configuration.GetSection("Fixer").GetSection("ApiKey").Value;
                var url = _configuration.GetSection("Fixer").GetSection("Baseurl").Value;

                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("ApiKey", apikey);

               
                var content = await client.GetStringAsync(url);
               

                return JsonConvert.DeserializeObject<FixerApiResponse>(content);
            }
            catch(Exception ex)
            {
                throw ex;
            }

        }
    }
}
