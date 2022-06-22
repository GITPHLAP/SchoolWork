using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SymmetricEncryptionTest_Console
{
    internal class Encrypter
    {

        public static SymmetricAlgorithm AlgorithmHandler(string input)
        {
            return input switch
            {
                "DES" => DES.Create(),
                "3DES" => TripleDES.Create(),
                "AES" => Aes.Create(),
                "RC2" => RC2.Create(),
                "Rijndael" => Rijndael.Create(),
                _ => null,
            };
        }

        public static byte[] Encrypt(SymmetricAlgorithm algorithm, string message)
        {
            byte[] msg = Encoding.ASCII.GetBytes(message);
            using MemoryStream ms = new();
            using CryptoStream cs = new(ms, algorithm.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(msg, 0, msg.Length);
            cs.Close();
            return ms.ToArray();
        }

        public static string Decrypt(SymmetricAlgorithm algorithm, string encryptedMsg, string key, string iv)
        {
            byte[] buffer = Convert.FromBase64String(encryptedMsg);

            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, algorithm.CreateDecryptor(Convert.FromBase64String(key), Convert.FromBase64String(iv)), CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
