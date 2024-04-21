using steamPy.Helpers.Extensions;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace steamPy.Helpers
{
    public static class EncryptHelper
    {
        public static string MD5Encrypt(string data,string salt)
        {
            if (data.IsNullOrEmpty()) return "";
            if (salt.IsNullOrEmpty()) return data;
            using MD5 md5 = MD5.Create();
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(data + salt));
            return Convert.ToBase64String(bytes);
        }

        // 加密字符串
        public static string DESEncrypt(string sInputString, string sKey = "niceboat")
        {
            byte[] data = Encoding.UTF8.GetBytes(sInputString);
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(sKey);
            DES.IV = Encoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return BitConverter.ToString(result);
        }
        // 解密字符串
        public static string DESDecrypt(string sInputString, string sKey = "niceboat")
        {
            string[] sInput = sInputString.Split("-".ToCharArray());
            byte[] data = new byte[sInput.Length];
            for (int i = 0; i < sInput.Length; i++)
            {
                data[i] = byte.Parse(sInput[i], NumberStyles.HexNumber);
            }
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.ASCII.GetBytes(sKey);
            DES.IV = Encoding.ASCII.GetBytes(sKey);
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(data, 0, data.Length);
            return Encoding.UTF8.GetString(result);
        }
    }
}
