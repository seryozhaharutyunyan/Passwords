using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using static System.Convert;

namespace Passwords.Helpers
{
    public static class EncodeDecodeHelper
    {
        public static string GenerateHash(string password, string slat = "")
        {
            using (SHA512 sha = SHA512.Create())
            {
                byte[] pHash = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(password)
                    );

                byte[] sHash = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(slat)
                    );

                string passwordHash = Encoding.Unicode.GetString(sHash) +
                    Encoding.Unicode.GetString(pHash);

                byte[] passwordData = sha.ComputeHash(
                    Encoding.Unicode.GetBytes(passwordHash)
                    );

                return Convert.ToBase64String(passwordData);
            }
        }

        public static bool ValidatePassword(string password, string passwordHash, string slat = "")
        {
            password = GenerateHash(password, slat);

            return password == passwordHash;
        }

        public static string Encrypt(string plainText, string password)
        {
            byte[] encryptedBytes;
            byte[] plainBytes = Encoding.Unicode.GetBytes(plainText);
            using (Aes aes = Aes.Create())
            {
                
                Stopwatch timer = Stopwatch.StartNew();
                using (Rfc2898DeriveBytes pbkdf2 = new(password, [1,1,1,1]))
                {
                    aes.Key = pbkdf2.GetBytes(32); 
                    aes.IV = pbkdf2.GetBytes(16);
                }
                timer.Stop();
                using (MemoryStream ms = new())
                {
                    using (ICryptoTransform transformer = aes.CreateEncryptor())
                    {
                        using (CryptoStream cs = new(
                        ms, transformer, CryptoStreamMode.Write))
                        {
                            cs.Write(plainBytes, 0, plainBytes.Length);
                        }
                    }
                    encryptedBytes = ms.ToArray();
                }
            }
            return ToBase64String(encryptedBytes);
        }
        public static string Decrypt(string cipherText, string password)
        {
            byte[] plainBytes;
            byte[] cryptoBytes = FromBase64String(cipherText);
            using (Aes aes = Aes.Create())
            {
                using (Rfc2898DeriveBytes pbkdf2 = new(password, [1,1,1,1]))
                {
                    aes.Key = pbkdf2.GetBytes(32);
                    aes.IV = pbkdf2.GetBytes(16);
                }
                using (MemoryStream ms = new())
                {
                    using (ICryptoTransform transformer = aes.CreateDecryptor())
                    {
                        using (CryptoStream cs = new(
                        ms, transformer, CryptoStreamMode.Write))
                        {
                            cs.Write(cryptoBytes, 0, cryptoBytes.Length);
                        }
                    }
                    plainBytes = ms.ToArray();
                }
            }
            return Encoding.Unicode.GetString(plainBytes);
        }
    }
}
