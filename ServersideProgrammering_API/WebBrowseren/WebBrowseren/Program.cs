using System;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebBrowseren
{
    class Program
    {
        static readonly HttpClient httpClient = new();
        static void Main(string[] args)
        {
            string html = ReturnWebSite("https://www.bt.dk/").Result;

            HtmlManager manager = new HtmlManager();

            Console.WriteLine(manager.GetFormattedHtml(html, new string[] { "script", "style","source" }));
            
        }

        static async Task<string> ReturnWebSite(string url)
        {
            // try getting the http response
            try
            {
                HttpResponseMessage response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                string body = response.Content.ReadAsStringAsync().Result;

                return body;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Error");
                return e.Message;
            }
        }
    }
}
