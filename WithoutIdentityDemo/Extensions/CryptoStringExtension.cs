using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WithoutIdentityDemo.Extensions
{
    public static class CryptoStringExtension
    {
        public static string Key { get; set; } = "12345678";
        public static string Iv { get; set; } = "12345678";
        public static string ToAesEncode(this string original)
        {
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] key = sha256.ComputeHash(Encoding.ASCII.GetBytes(Key));
            byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(Iv));
            

            return ToAesEncode(original, key, iv);
        }

        public static string ToAesEncode(this string original,byte[] key, byte[] iv)
        {
            var encode = "";
            var aes = new AesCryptoServiceProvider();

            aes.Key = key;
            aes.IV = iv;
            var dataArray = Encoding.UTF8.GetBytes(original);
            using (var ms = new MemoryStream())
            using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
            {
                cs.Write(dataArray, 0, dataArray.Length);
                cs.FlushFinalBlock();
                encode = Convert.ToBase64String(ms.ToArray());
            }
            return encode;
        }

        public static string ToDecode(this string original)
        {
            SHA256CryptoServiceProvider sha256 = new SHA256CryptoServiceProvider();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();

            byte[] key = sha256.ComputeHash(Encoding.ASCII.GetBytes(Key));
            byte[] iv = md5.ComputeHash(Encoding.ASCII.GetBytes(Iv));


            return ToDecode(original, key, iv);
        }

        public static string ToDecode(this string original, byte[] key, byte[] iv)
        {

            var encode = "";
            try
            {
                var aes = new AesCryptoServiceProvider();

                aes.Key = key;
                aes.IV = iv;
                var dataArray = Convert.FromBase64String(original);
                using (var ms = new MemoryStream())
                using (var cs = new CryptoStream(ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(dataArray, 0, dataArray.Length);
                    cs.FlushFinalBlock();
                    encode = Encoding.UTF8.GetString(ms.ToArray());
                }
            }
            catch(Exception ex)
            {

                encode = ex.Message;
            }

            return encode;
        }


    }
}
