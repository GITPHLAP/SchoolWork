using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tilfældigheder
{
    public class Encrypter
    {
        public static char[] normalAlpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZÆØÅ".ToCharArray();
        //public static char[] cryptedAlpha = "KLMNOPQRSTUVWXYZÆØÅABCDEFGHIJ".ToCharArray();
        
        public static string Encrypt(string message)
        {
            string encryptedMessage = "";
            foreach (var c in message.ToUpper().ToCharArray())
            {
                if (c == ' ')
                {
                    encryptedMessage += " ";
                }
                else
                {
                    // Index in normal alpha
                    int index = Array.IndexOf(normalAlpha, c);
                    if (index != -1)
                    {
                        encryptedMessage += normalAlpha[
                            index == (normalAlpha.Length - 1) ? 0 : index + 1];
                    }
                }

            }

            return encryptedMessage;
        }

        public static string Decrypt(string encryptedMsg)
        {
            string decryptedMessage = "";
            foreach (var c in encryptedMsg.ToUpper().ToCharArray())
            {
                if (c == ' ')
                {
                    decryptedMessage += " ";
                }
                else
                {
                    // Index in normal alpha
                    int index = Array.IndexOf(normalAlpha, c);
                    if (index != -1)
                    {
                        decryptedMessage += normalAlpha[
                        index == 0 ? (normalAlpha.Length - 1) : index - 1];
                    }
                }
            }

            return decryptedMessage;
        }
    }
}
