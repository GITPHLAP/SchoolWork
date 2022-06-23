using RSACrypto;
using System;
using System.Security.Cryptography;

namespace RSASender
{
    internal class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                // Get public data
                Console.WriteLine("Write exponent:");
                string eksponent = Console.ReadLine();

                Console.WriteLine("Write modulus:");
                string modulus = Console.ReadLine();
                // Create rsa provider
                using (RSACryptoServiceProvider rsa = RSAEncryptor.GetPublicKeyProvider(RSAEncryptor.BitConReverse(modulus), RSAEncryptor.BitConReverse(eksponent)))
                {
                    var test = rsa.ExportParameters(false);

                    // Insert cipherbytes
                    Console.WriteLine("Write message");
                    string plaintext = Console.ReadLine();

                    // Encrypt message
                    Console.WriteLine("Encrypted message");
                    Console.WriteLine($"{RSAEncryptor.EncryptString(rsa, plaintext)}");
                }

                Console.WriteLine("Press enter to try again ");
                Console.ReadLine();
                Console.Clear();
            }

        }
    }
}
