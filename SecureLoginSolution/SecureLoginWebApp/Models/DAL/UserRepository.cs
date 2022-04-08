using System.Net.Http;
using System;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SecureLoginWebApp.Models.DAL
{
    public class UserRepository : IUserRepository
    {
        HttpClient httpClient = new HttpClient();

        public UserRepository()
        {
            httpClient.BaseAddress = new Uri(Environment.GetEnvironmentVariable("API_URL"));
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

        }

        public async void CreateUser(ApplicationUser user)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(
                $"api/user/new", user);
            response.EnsureSuccessStatusCode();
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

        public async void UpdateUser(ApplicationUser user)
        {
            HttpResponseMessage response = await httpClient.PutAsJsonAsync(
                $"api/user/update", user);
            response.EnsureSuccessStatusCode();
        }

        public async Task<ApplicationUser> FindUserByName(string name)
        {
            HttpResponseMessage response = await httpClient.GetAsync(
                $"api/user/FindByName?username={name}");
            if (response.IsSuccessStatusCode)
            {
                //var test = await response.Content.ReadFromJsonAsync<ApplicationUser>();
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            return null;
        }

        public async Task<ApplicationUser> FindUserByEmail(string email)
        {
            //TODO: Find the right path for FindUserByEmail
            HttpResponseMessage response = await httpClient.GetAsync(
    $"api/user/FindByEmail?email={email}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            return null;
        }

        public async Task<ApplicationUser> FindUserById(string userId)
        {
            //TODO: Find the right path for FindUserById

            HttpResponseMessage response = await httpClient.GetAsync(
    $"api/user/FindById?id={userId}");

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ApplicationUser>();
            }
            return null;

            
        }

        public async void DeleteUser(int userId)
        {
            //TODO: Find the right path for FindUserById

            HttpResponseMessage response = await httpClient.DeleteAsync(
    $"api/user?userid={userId}");

            response.EnsureSuccessStatusCode();
        }
        
    }
}
