using System.Security.Cryptography;
using System.Text;

namespace PersonalPasswordManager.CommonFunctions
{
    public class EncryptionDecryption
    {
        const string key = "81F7E2E97B9E43D3A42F3184B23887CA";
        public static string EncryptString( string plainText)
        {
            if (string.IsNullOrEmpty(plainText)) { return ""; }
            byte[] iv = new byte[16], array;
            using (Aes aes = Aes.Create())
            {
                aes.Key = Encoding.UTF8.GetBytes(key);
                aes.IV = iv;
                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
                using MemoryStream memoryStream = new();
                using CryptoStream cryptoStream = new(memoryStream, encryptor, CryptoStreamMode.Write);
                using (StreamWriter streamWriter = new(cryptoStream))
                {
                    streamWriter.Write(plainText);
                }
                array = memoryStream.ToArray();
            }
            return Convert.ToBase64String(array);
        }

        public static string DecryptString( string cipherText)
        {
            if (string.IsNullOrEmpty(cipherText)) { return ""; }
            byte[] iv = new byte[16], buffer = Convert.FromBase64String(cipherText);
            using Aes aes = Aes.Create();
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = iv;
            ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            using MemoryStream memoryStream = new(buffer);
            using CryptoStream cryptoStream = new(memoryStream, decryptor, CryptoStreamMode.Read);
            using StreamReader streamReader = new(cryptoStream);
            return streamReader.ReadToEnd();
        }
    }
}
