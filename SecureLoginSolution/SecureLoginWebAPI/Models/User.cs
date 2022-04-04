using System;

namespace SecureLoginWebAPI.Models
{
    public class User
    {
        private string userName;
        private string plain_password;
        private string salt;

        public User(string userName, string plain_password)
        {
            this.userName = userName;
            this.plain_password = plain_password;
        }

        public string UserName { get => userName; set => userName = value; }
        public string Plain_password { get => plain_password;  set => plain_password = value; }

        public string Hashed_Password
        {
            get
            {
                //Get salt 
                byte[] salt_b = Hash.GenerateSalt();
                salt = Convert.ToBase64String(salt_b);

                return Convert.ToBase64String(
                    Hash.HashPasswordWithSalt(plain_password, salt_b));
            }
        }

        public string Salt { get => salt; }

    }
}
