using System;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;

namespace MAC_Tester_Console
{
    internal class Program
    {
        static int keySize = 32;
        static void Main(string[] args)
        {
            Stopwatch sw = new Stopwatch();
            HashAlgorithm algorithm;
            //Select between compute and verify
            Console.WriteLine("Write Verify...  Else compute will be selected");
            Console.WriteLine("Verify is only for HMAC");
            bool verify = Console.ReadLine() == "Verify";

            // TODO: Show time ellapsed for computed

            if (verify)
            {
                Console.WriteLine("Write key");
                string key = Console.ReadLine();

                Console.WriteLine("Select between:");
                Console.WriteLine("MD5, SHA1, SHA256, SHA384, SHA512");

                // User pick Mac
                algorithm = MACHandler(Console.ReadLine(), Encoding.ASCII.GetBytes(key));
                Console.WriteLine("Write message");
                string plaintext = Console.ReadLine();

                Console.WriteLine("Write hashedMAC");
                string mac = Console.ReadLine();

                bool result = VerifyMAC(plaintext, ComputeMAC(plaintext, algorithm), algorithm);

                Console.WriteLine(result);
            }
            else
            {
                byte[] key = null;
                Console.WriteLine("Write HMAC else normal HASH will be selected:");
                string input = Console.ReadLine();
                if (input == "HMAC")
                {
                    key = GenerateKey();
                    Console.WriteLine(Convert.ToBase64String(key));
                }


                Console.WriteLine("Select between:");
                Console.WriteLine("MD5, SHA1, SHA256, SHA384, SHA512");

                // User pick Mac
                algorithm = MACHandler(Console.ReadLine(), key);

                Console.WriteLine("Write message");
                string plaintext = Console.ReadLine();

                Console.WriteLine($"HEX    {ComputeMAC(plaintext, algorithm, true)}");
                Console.WriteLine($"Base64    {ComputeMAC(plaintext, algorithm, false)}");



                //Console.WriteLine("Verifying");
                //bool result = VerifyMAC(plaintext, ComputeMAC(plaintext, algorithm), algorithm);

                //Console.WriteLine(result);
            }



        }


        static HashAlgorithm MACHandler(string input, byte[] HMACKey = null)
        {
            switch (input)
            {
                case "MD5":
                    return HMACKey != null ? new HMACMD5(): MD5.Create();
                case "SHA1":
                    return HMACKey != null ? new HMACSHA1() : SHA1.Create();
                case "SHA256":
                    return HMACKey != null ? new HMACSHA256() : SHA256.Create();
                case "SHA384":
                    return HMACKey != null ? new HMACSHA384() : SHA384.Create();
                case "SHA512":
                    return HMACKey != null ? new HMACSHA512() : SHA512.Create();
                default:
                    return null;
            }
        }

        static byte[] GenerateKey()
        {
            RandomNumberGenerator rnd = RandomNumberGenerator.Create();
            // 32 in key size
            byte[] key = new byte[keySize];
            rnd.GetBytes(key);
            return key;

        }

        static string ComputeMAC(string message, HashAlgorithm algorithm, bool AsHEX)
        {
            return AsHEX ?  Convert.ToHexString(ComputeMAC(message, algorithm)):
                Convert.ToBase64String(ComputeMAC(message, algorithm));
        }

        static byte[] ComputeMAC(string message, HashAlgorithm algorithm)
        {
            Stopwatch sw = new Stopwatch();
            byte[] msg = Encoding.Default.GetBytes(message);
            sw.Start();
            var result = algorithm.ComputeHash(msg);
            sw.Stop();
            Console.WriteLine($"Time: {sw.Elapsed}");
            return result;
        }

        static bool VerifyMAC(string plaintext, byte[] computedMac, HashAlgorithm algorithm)
        {
            return CompareByteArrays(ComputeMAC(plaintext, algorithm), computedMac, algorithm.HashSize / 8);
        }

        static bool CompareByteArrays(byte[] a, byte[] b, int len)
        {
            for (int i = 0; i < len; i++)
            {
                if (a[i] != b[i]) return false;
            }
            return true;
        }
    }
}
