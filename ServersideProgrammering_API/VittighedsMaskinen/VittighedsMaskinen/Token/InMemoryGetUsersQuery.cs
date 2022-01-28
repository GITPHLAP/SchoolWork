using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VittighedsMaskinen.Authentication;

namespace VittighedsMaskinen.Token
{
    public class InMemoryGetUsersQuery
    {
        private readonly List<User> _userCred;

        public InMemoryGetUsersQuery()
        {
            List<User> existingUsers = new List<User>();
            existingUsers.Add(new User() { UserName = "Admin", Password = "1234", Roles = ( new List<string> { "Everything" } )});
            existingUsers.Add(new User() { UserName = "User", Password = "4321", Roles = ( new List<string> { "DadJokes" } )});

            _userCred = existingUsers;
        }

        public User Validate(string username, string password)
        {
            User user = _userCred.Find(u => u.UserName == username && u.Password == password);
            return user;
        }
    }
}
