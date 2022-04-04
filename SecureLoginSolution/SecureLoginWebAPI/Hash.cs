using System.Security.Cryptography;
using System;
using System.Text;

namespace SecureLoginWebAPI
{
    public class Hash
    {
        public static byte[] GenerateSalt()
        {
            const int saltLength = 32;

            using (var randomNumberGenerator = new RNGCryptoServiceProvider())
            {
                var randomNumber = new byte[saltLength];
                randomNumberGenerator.GetBytes(randomNumber);

                return randomNumber;
            }
        }

        private static byte[] Combine(byte[] first, byte[] second)
        {
            var ret = new byte[first.Length + second.Length];

            Buffer.BlockCopy(first, 0, ret, 0, first.Length); //set new array with first array 
            Buffer.BlockCopy(second, 0, ret, first.Length, second.Length); // add the second array to ret begin from first array ending 

            return ret;
        }
        
        public static byte[] HashPasswordWithSalt(string toBeHashed, byte[] salt)
        {
            byte[] toBeHashed_B = Encoding.UTF8.GetBytes(toBeHashed);
            using (var sha256 = SHA256.Create())
            {
                return sha256.ComputeHash(Combine(toBeHashed_B, salt));
            }
        }
    }
}
