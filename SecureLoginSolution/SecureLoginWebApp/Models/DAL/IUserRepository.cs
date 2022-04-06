using System.Threading.Tasks;

namespace SecureLoginWebApp.Models.DAL
{
    public interface IUserRepository
    {
        Task<bool> IsUserNameUnique(string username);
        void CreateUser(User user);
        void CreateUser(ApplicationUser user);
        Task<string> ValidateUserCredentials(User user);
        void UpdateUser(ApplicationUser user);

        Task<ApplicationUser> FindUserByEmail(string email);
        Task<ApplicationUser> FindUserById(string userId);
        Task<ApplicationUser> FindUserByName(string name);


    }
}
