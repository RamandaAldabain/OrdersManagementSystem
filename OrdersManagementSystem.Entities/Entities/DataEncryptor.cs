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
            byte[] encryptedData = ProtectedData.Protect(Encoding.UTF8.GetBytes(data.ToString()), null, DataProtectionScope.CurrentUser);
            return Convert.ToBase64String(encryptedData);
        }

        public static T DecryptData<T>(string encryptedData)
        {
            byte[] decryptedData = ProtectedData.Unprotect(Convert.FromBase64String(encryptedData), null, DataProtectionScope.CurrentUser);
            return (T)Convert.ChangeType(Encoding.UTF8.GetString(decryptedData), typeof(T));
        }
    }
}
