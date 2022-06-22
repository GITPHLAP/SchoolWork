using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace SymmetricEncryptionTest_Console
{
    internal class Program
    {
        static int keySize = 32;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            SymmetricAlgorithm algorithm;
            //Select between compute and verify
            Console.WriteLine("Write Decrypt...  Else Encrypt will be selected");
            bool decrypt = Console.ReadLine() == "Decrypt";

            // TODO: Show time ellapsed for computed

            if (decrypt)
            {


                Console.WriteLine("Select between:");
                Console.WriteLine("DES, 3DES, AES, RC2, Rijndael");

                // User pick Mac
                algorithm = Encrypter.AlgorithmHandler(Console.ReadLine());

                Console.WriteLine($"Write IV:");
                string iv = Console.ReadLine();
                Console.WriteLine($"Write Key:");
                string key = Console.ReadLine();
                Console.WriteLine("Write encrypted message");
                string cipher = Console.ReadLine();

                sw.Start();
                string plaintext = Encrypter.Decrypt(algorithm, cipher, key, iv);
                sw.Stop();

                Console.WriteLine(plaintext);
                Console.WriteLine("Time: " + sw.Elapsed); 

            }
            else
            {

                Console.WriteLine("Select between:");
                Console.WriteLine("DES, 3DES, AES, RC2, Rijndael");

                // User pick Mac
                algorithm = Encrypter.AlgorithmHandler(Console.ReadLine());

                Console.WriteLine("Write message");
                string plaintext = Console.ReadLine();
                sw.Start();
                byte[] encrypted = Encrypter.Encrypt(algorithm, plaintext);
                sw.Stop();
                Console.WriteLine("Time: " + sw.Elapsed);
                Console.WriteLine($"HEX    {Convert.ToHexString(encrypted)}");
                Console.WriteLine($"Base64    {Convert.ToBase64String(encrypted)}");

                Console.WriteLine($"IV: {Convert.ToBase64String(algorithm.IV)}");
                Console.WriteLine($"Key: {Convert.ToBase64String(algorithm.Key)}");

            }



        }
    }
}
