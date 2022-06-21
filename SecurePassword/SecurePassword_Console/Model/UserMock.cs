using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurePassword_Console.Model
{
    public class UserMock
    {
        public UserMock(string userName, string password, string salt, int loginAttempts)
        {
            UserName = userName;
            Password = password;
            Salt = salt;
            BadLoginAttempts = loginAttempts;
        }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Salt { get; set; }
        public int BadLoginAttempts { get; set; }
        public bool Lock { get { return BadLoginAttempts >= 5; }  }


        public static List<UserMock> GetUsers()
        {
            List<UserMock> users = new List<UserMock>();
            string salt = Convert.ToBase64String(PBKD.GenerateSalt());
            users.Add(new UserMock("Jens", 
                Convert.ToBase64String(PBKD.ComputeHash("Kode1234", salt)),
                salt,
                0));
            salt = Convert.ToBase64String(PBKD.GenerateSalt());
            users.Add(new UserMock("Erik",
                Convert.ToBase64String(PBKD.ComputeHash("Kode1234", salt)),
                salt,
                0));
            salt = Convert.ToBase64String(PBKD.GenerateSalt());
            users.Add(new UserMock("Bo",
                Convert.ToBase64String(PBKD.ComputeHash("Kode1234", salt)),
                salt,
                0));
            return users;
        }
    }
}
