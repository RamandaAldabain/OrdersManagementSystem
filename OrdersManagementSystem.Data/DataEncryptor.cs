using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace OrdersManagementSystem.Entities
{
    public static class DataEncryptor
    {
        public static string EncryptData<T>(T data)
        {
            byte[] iv = new byte[16];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(iv);
            }

            // Encrypt the data with the random IV
            byte[] encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(data.ToString()), iv, DataProtectionScope.CurrentUser);

            // Combine IV and encrypted data into a single byte array
            byte[] combinedData = new byte[iv.Length + encryptedData.Length];
            Array.Copy(iv, combinedData, iv.Length);
            Array.Copy(encryptedData, 0, combinedData, iv.Length, encryptedData.Length);

            // Return the combined data as a base64 string
            return Convert.ToBase64String(combinedData);
        }

        public static T DecryptData<T>(string encryptedData)
        {
            byte[] combinedData = Convert.FromBase64String(encryptedData);

            // Extract IV from the combined data
            byte[] iv = new byte[16];
            Array.Copy(combinedData, iv, iv.Length);

            // Extract encrypted data from the combined data
            byte[] encryptedBytes = new byte[combinedData.Length - iv.Length];
            Array.Copy(combinedData, iv.Length, encryptedBytes, 0, encryptedBytes.Length);

            // Decrypt the data using the extracted IV
            byte[] decryptedBytes = ProtectedData.Unprotect(encryptedBytes, iv, DataProtectionScope.CurrentUser);

            // Convert decrypted bytes to string and then to the generic type T
            string decryptedString = Encoding.UTF8.GetString(decryptedBytes);
            return (T)Convert.ChangeType(decryptedString, typeof(T));
        }
    }
}
