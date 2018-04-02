using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApiGateway.App_Start.Crypt
{
    public class StringMD5
    {
        /// <summary>
        /// MD5 16位加密
        /// </summary>
        public static string GetMd5Str16(string str)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            string code = BitConverter.ToString(md5.ComputeHash(UTF8Encoding.Default.GetBytes(str)), 4, 8);
            code = code.Replace("-", string.Empty);
            return code;
        }

        /// <summary>
        /// MD5　32位加密
        /// </summary>
        public static string GetMd5Str32(string str)
        {
            string code = string.Empty;

            System.Security.Cryptography.MD5 md5 = System.Security.Cryptography.MD5.Create();
            byte[] s = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            // 通过使用循环，将字节类型的数组转换为字符串，此字符串是常规字符格式化所得
            for (int i = 0; i < s.Length; i++)
            {
                // 将得到的字符串使用十六进制类型格式。格式后的字符是小写的字母，如果使用大写（X）则格式后的字符是大写字符 
                code = code + s[i].ToString("X");
            }

            return code;
        }
    }
}