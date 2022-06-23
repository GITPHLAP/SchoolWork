using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace RSACrypto
{
    public class RSAEncryptor
    {
        /// <summary>
        /// Create private/public key pair 
        /// When 
        /// </summary>
        /// <returns>Public/Private Keypair</returns>
        public static RSACryptoServiceProvider GetKeyPair()
        {
            RSACryptoServiceProvider _rsa;

            // Create both public and private key
            _rsa = new RSACryptoServiceProvider(4096);

            return _rsa;
        }


        /// <summary>
        /// Use to create a provider to encrypt a message
        /// </summary>
        /// <param name="modulus"></param>
        /// <param name="eksponent"></param>
        /// <returns></returns>
        public static RSACryptoServiceProvider GetPublicKeyProvider(byte[] modulus, byte[] eksponent)
        {

            // Create key informations 
            RSAParameters rsaKeyInfo = new RSAParameters();
            rsaKeyInfo.Modulus = modulus;
            rsaKeyInfo.Exponent = eksponent;


            RSACryptoServiceProvider _rsa = new(4096);

            // Import key info
            _rsa.ImportParameters(rsaKeyInfo);

            return _rsa;
        }

        /// <summary>
        /// Encrypt a string with given rsa keypair
        /// </summary>
        /// <param name="_rsa">KeyPair</param>
        /// <param name="message"></param>
        /// <returns>Encrypted base64 string</returns>
        public static string EncryptString(RSACryptoServiceProvider _rsa, string message)
        {
            byte[] encryptedMsg = _rsa.Encrypt(Encoding.UTF8.GetBytes(message), true);
            return BitConverter.ToString(encryptedMsg);
        }

        /// <summary>
        /// Decrypt a string with given rsa keypair
        /// </summary>
        /// <param name="_rsa"> KeyPair</param>
        /// <param name="encryptedMsg">Should be Base64 string</param>
        /// <returns>Decrypted message</returns>
        public static string DecryptString(RSACryptoServiceProvider _rsa, byte[] encryptedMsg)
        {
            byte[] decryptedMsg = _rsa.Decrypt(encryptedMsg, true);
            return Encoding.UTF8.GetString(decryptedMsg);
        }

        public static byte[] BitConReverse(string input)
{
            string[] arr = input.Split('-');
            byte[] byteArr = new byte[arr.Length];
            for (int i = 0; i < arr.Length; i++)
            {
                byteArr[i] = Convert.ToByte(arr[i], 16);
            }

            return byteArr;
        }
    }
}
