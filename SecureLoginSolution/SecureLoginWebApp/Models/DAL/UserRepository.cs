using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace SecureLoginWebApp.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        HttpClient httpClient = new HttpClient();

        public UserRepository()
        {
            httpClient.BaseAddress = new Uri("http://localhost:5000/");
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async void CreateUser(User user)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                $"api/user", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> IsUserNameUnique(string username)
        {
            bool result = false;
            HttpResponseMessage response = await httpClient.GetAsync(
                $"api/user/{username}");
            if (response.IsSuccessStatusCode)
            {
                result = bool.Parse(await response.Content.ReadAsStringAsync());
            }
            return result;
        }

        public async Task<string> ValidateUserCredentials(User user)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                $"api/login", user);

            if (response.IsSuccessStatusCode)
            {
                return null;
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
