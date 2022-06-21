using SecurePassword_Console.Model;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SecurePassword_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<UserMock> Users = UserMock.GetUsers();

            #region Login
            while(true)
            {
                Console.WriteLine("Welcome!");
                Console.WriteLine("Write username");
                // UserName
                string userName = Console.ReadLine();

                // Check for username
                UserMock user = Users.Find(u => u.UserName == userName);
                if (user is UserMock)
                {
                    while (!user.Lock)
                    {
                        Console.WriteLine("Write Password");
                        string password = Console.ReadLine();

                        if (!user.Lock && PBKD.VerifyHash(password, user))
                        {
                            Console.WriteLine("Login Success");
                            Thread.Sleep(2000);
                            Console.Clear();
                            break;
                        }
                        else
                        {
                            // Log to "DB"
                            BadLogin(user);
                            Console.WriteLine("Wrong passwrod... Try again");
                        }
                    }
                    if (user.BadLoginAttempts >= 5)
                    {
                        Console.Clear();
                        Console.WriteLine($"User: {user.UserName} LOGOUT");
                    }
                }
                // Clear console if user not exists
                else
                    Console.Clear();

            }
            #endregion
        }

        static void BadLogin(UserMock user)
        {
            user.BadLoginAttempts++;
        }
    }
}
