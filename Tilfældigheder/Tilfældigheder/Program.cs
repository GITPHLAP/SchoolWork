using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Tilfældigheder
{
    internal class Program
    {
        static int iterations = 1000000;
        static void Main(string[] args)
        {
            //string encryptedMsg = Encrypter.Encrypt("Hej Kris!");
            //Console.WriteLine(encryptedMsg);
            //string decryptedMsg = Encrypter.Decrypt(encryptedMsg);
            //Console.WriteLine(decryptedMsg);

            Stopwatch pseudosw = new Stopwatch();
            Stopwatch cryptosw = new Stopwatch();
            List<int> pseudoresults = new List<int>();
            List<int> cryptoresults = new List<int>();
            Console.WriteLine("Crypto");
            cryptosw.Start();
            for (int i = 0; i < iterations; i++)
            {
                cryptoresults.Add(SecureRandom());
            }
            cryptosw.Stop();
            CalculateConvidence(cryptoresults);
            Console.WriteLine($"Time: {cryptosw.Elapsed} seconds");

            // Pseudo
            Console.WriteLine("PSEUDO");
            pseudosw.Start();
            for (int i = 0; i < iterations; i++)
            {
                pseudoresults.Add(PseudoRandom());
            }
            pseudosw.Stop();
            CalculateConvidence(pseudoresults);
            Console.WriteLine($"Time: {pseudosw.Elapsed} seconds");
        }

        static int SecureRandom()
        {
            
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] data = new byte[4];
            rng.GetBytes(data);
            return BitConverter.ToInt32(data, 0);
        }

        static int PseudoRandom()
        {
            Random random = new Random();

            return random.Next();
        }

        static void CalculateConvidence(List<int> results)
        {
            Console.WriteLine("Convidence more then 1");
            var test = results.GroupBy(r => r);
            var newList = test.Select(r => new
            {
                Id = r.Key
            }).ToList();


            var memberKeys = results.GroupBy(x => x)
                .Where(r => r.Count() > 1)
                           .Select(grp => new
                           {
                               Id = grp.Key,
                               Count = grp.Count()
                           })
                           .ToList();
            Console.WriteLine("Counts " + memberKeys.Count);
            //foreach (var item in memberKeys)
            //{
            //    Console.WriteLine($"{item.Id}: count {item.Count}");

            //}
        }
    }
}
