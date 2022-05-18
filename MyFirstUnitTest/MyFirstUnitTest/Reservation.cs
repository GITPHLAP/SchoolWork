using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFirstUnitTest
{
    public class Reservation
    {
        public User MadeBy { get; set; }

        public bool CanBeCancelledBy(User user)
        {
            if (user.IsAdmin)
            {
                return true;
            }

            if (MadeBy == user)
            {
                return true;
            }

            return false;
        }

        public Reservation(User madeBy)
        {
            MadeBy = madeBy;
        }
    }

    public class User
    {
        public bool IsAdmin { get; set; }

        public User(bool isAdmin = false)
        {
            IsAdmin = isAdmin;
        }
    }
}
