using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Newtonsoft.Json;

namespace GetDataFromRoadInfo.DAL
{
    class HttpRequestManager
    {

        public class TokenResponse
        {
            private string token_type;
            private int expires_in;
            private int ext_expires_in;
            private string access_token;

            public string Token_type { get => token_type; set => token_type = value; }
            public int Expires_in { get => expires_in; set => expires_in = value; }
            public int Ext_expires_in { get => ext_expires_in; set => ext_expires_in = value; }
            public string Access_token { get => access_token; set => access_token = value; }
        }

        string token;
        DateTimeOffset tokenExpireTime;
        HttpClient client;
        string downloadUrl;
        string tenantId;
        string tokenUrl;
        string serviceAccountId;
        string serviceAccountPass;

        public HttpRequestManager()
        {
            DotNetEnv.Env.Load();

            client = new HttpClient();
            downloadUrl = Environment.GetEnvironmentVariable("DOWNLOAD_URL");
            tenantId = Environment.GetEnvironmentVariable("TENANT_ID");
            tokenUrl = Environment.GetEnvironmentVariable("TOKEN_URL");
            serviceAccountId = Environment.GetEnvironmentVariable("SERVICE_ACCOUNT_ID");
            serviceAccountPass = Environment.GetEnvironmentVariable("SERVICE_ACCOUNTPASS");
        }


        /// <summary>
        /// Get the token and set the global token field and token expiredTime
        /// </summary>
        /// <returns></returns>
        private async Task GetToken()
        {
            //Prepare body for the request
            string data = $"client_id={serviceAccountId}" +
                $"& scope ={serviceAccountId}/.default" +
                $"& client_secret ={serviceAccountPass}" +
                "& grant_type = client_credentials";

            var body = new StringContent(data, Encoding.UTF8, "application/x-www-form-urlencoded");

            //TODO: Add error handling


            using (var response = await client.PostAsync(tokenUrl, body))
            {

                if (response.IsSuccessStatusCode)
                {
                    var contentStream = await response.Content.ReadAsStreamAsync();

                    using (var streamReader = new StreamReader(contentStream))
                    {
                        using (var jsonReader = new JsonTextReader(streamReader))
                        {
                            JsonSerializer serializer = new JsonSerializer();

                            try
                            {
                                TokenResponse test = serializer.Deserialize<TokenResponse>(jsonReader);
                                token = test.Access_token;
                                tokenExpireTime = DateTimeOffset.UtcNow.AddSeconds(test.Expires_in);
                            }
                            catch (JsonReaderException)
                            {
                                Scheduler.WriteToErrorLog("Invalid JSON.", System.Diagnostics.EventLogEntryType.Error);

                            }
                        }
                    }
                }
                else
                {
                    //Write something in errorlog
                    Scheduler.WriteToErrorLog("[Token]" + response.StatusCode.ToString(), System.Diagnostics.EventLogEntryType.Warning);
                }
            }
        }

        /// <summary>
        /// Method to get the xml data and new token if it expired
        /// </summary>
        /// <returns></returns>
        public async Task<string> GetData()
        {
            //Get token if its expire -20 is a buffer
            if (token == null || tokenExpireTime.AddSeconds(-20) == DateTimeOffset.UtcNow)
            {
                await GetToken();
            }

            using (var request = new HttpRequestMessage(HttpMethod.Get, downloadUrl))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);

                using (var response = client.SendAsync(request))
                {
                    if (response.Result.IsSuccessStatusCode)
                    {
                        return await response.Result.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        //TODO: Error handling
                        Scheduler.WriteToErrorLog("[GetData]" + response.Result.StatusCode.ToString(), System.Diagnostics.EventLogEntryType.Warning);
                        return null;
                    }
                }
            }
        }
    }
}
