using SecurePassword_Console.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SecurePassword_Console
{
    public class PBKD
    {
        static int keySize = 32;
        public static byte[] GenerateSalt()
        {
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            // 32 in key size
            byte[] key = new byte[keySize];
            rnd.GetBytes(key);
            return key;

        }

        public static byte[] ComputeHash(string password, string salt, int iterations = 100000, int hashByteSize = 32)
        {
            using (Rfc2898DeriveBytes rfc = new Rfc2898DeriveBytes(password, Convert.FromBase64String(salt), iterations, HashAlgorithmName.SHA512))
            {
                return rfc.GetBytes(hashByteSize);
            }
        }

        public static bool VerifyHash(string plaintext, UserMock user )
        {
            string hashedPlainText = Convert.ToBase64String(ComputeHash(plaintext, user.Salt));
            return hashedPlainText.Equals(user.Password);
        }
    }
}
