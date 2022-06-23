using RSACrypto;
using System;
using System.Security.Cryptography;
using static System.Net.Mime.MediaTypeNames;

namespace RSAReceiver
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                // Create rsa provider
                using (RSACryptoServiceProvider rsa = RSAEncryptor.GetKeyPair())
                {
                    var rsaParameters = rsa.ExportParameters(true);
                    // Show public data
                    Console.WriteLine($"Exponent: {BitConverter.ToString(rsaParameters.Exponent)}");
                    Console.WriteLine($"Modulus: {BitConverter.ToString(rsaParameters.Modulus)} \n\n");

                    // Show private data
                    Console.WriteLine($"D: {BitConverter.ToString(rsaParameters.D)} \n");
                    Console.WriteLine($"DP: {BitConverter.ToString(rsaParameters.DP)}  \n");
                    Console.WriteLine($"DQ: {BitConverter.ToString(rsaParameters.DQ)}  \n");
                    Console.WriteLine($"Inverse Q: {BitConverter.ToString(rsaParameters.InverseQ)}  \n");
                    Console.WriteLine($"P: {BitConverter.ToString(rsaParameters.P)}   \n");
                    Console.WriteLine($"Q: {BitConverter.ToString(rsaParameters.Q)}   \n");

                    // Insert cipherbytes
                    Console.WriteLine("Write cipher");
                    string ciphertxt = Console.ReadLine();

                    // Show the decrypted message
                    Console.WriteLine();
                    Console.WriteLine("Decrypted message: ");
                    Console.WriteLine(RSAEncryptor.DecryptString(rsa, RSAEncryptor.BitConReverse(ciphertxt)));
                }

                Console.WriteLine("Press enter to try again");
                Console.ReadLine();
                Console.Clear();

            }

        }
    }
}
