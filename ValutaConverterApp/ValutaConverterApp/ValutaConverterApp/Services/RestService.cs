using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using ValutaConverterApp.Models;

namespace ValutaConverterApp.Services
{
    public class RestService
    {
        HttpClient _client;

        public RestService()
        {
            _client = new HttpClient();
        }

        public async Task<ExchangeRate> GetRate(string from, string to)
        {
            ExchangeRate rate = new ExchangeRate($"{from}_{to}");
            var builder = new UriBuilder("https://free.currconv.com/api/v7/convert");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["q"] = rate.Name;
            query["compact"] = "ultra";
            query["apiKey"] = "fc8124044921745feb7e";

            builder.Query = query.ToString();

            string url = builder.ToString();

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (content != "{}")
                    {
                        rate.Rate = (double)JObject.Parse(content)[rate.Name];
                    }
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("\tERROR {0}", e.Message);
            }

            return await Task.FromResult(rate);
        }

        public async Task<List<Currency>> GetCurrencies()
        {
            var builder = new UriBuilder("https://free.currconv.com/api/v7/currencies");
            var query = HttpUtility.ParseQueryString(builder.Query);

            query["apiKey"] = "fc8124044921745feb7e";

            builder.Query = query.ToString();

            string url = builder.ToString();

            try
            {
                HttpResponseMessage response = await _client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    
                    // Dictionary is needed to convert the strange JSON response to list of currencies
                    Dictionary<string, Dictionary<string, Currency>> dic =
                        JsonConvert.DeserializeObject<Dictionary<string, Dictionary<string, Currency>>>(content);

                    return dic["results"].Values.ToList();
                }
            }
            catch (Exception e)
            {

                Debug.WriteLine("\tERROR {0}", e.Message);
            }

            return null;
        }

        public async Task<List<Currency>> GetSearchedCurrencies(string query)
        {

            return (await GetCurrencies())
                .Where(c => c.CurrencyName.ToLowerInvariant().Contains(query.ToLowerInvariant()))
                .ToList();
        }
    }
}
