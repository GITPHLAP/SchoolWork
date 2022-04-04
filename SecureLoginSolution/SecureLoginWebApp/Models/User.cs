

namespace SecureLoginWebApp.Models
{
    public class User
    {
        private string userName;
        private string plain_password;

        public User(string userName, string plain_password)
        {
            this.userName = userName;
            this.plain_password = plain_password;
        }

        public string UserName { get => userName; set => userName = value; }
        public string Plain_password { get => plain_password; set => plain_password = value; }
    }
}
