using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start.Crypt
{
    public class SHA
    {
        /// <summary>
        /// 加密
        /// </summary>
        /// <param name="data">需要加密是数据</param>
        /// <param name="key">加密key</param>
        /// <returns></returns>
        public static string Encrypt(string data, string key)
        {
            //对key做sha256加密
            byte[] sha256Data = System.Text.Encoding.UTF8.GetBytes(key);
            System.Security.Cryptography.SHA256Managed sha256 = new System.Security.Cryptography.SHA256Managed();
            string sha256Key = Convert.ToBase64String(sha256.ComputeHash(sha256Data));

            //当前取加密key中的某个元素索引
            int index = 0;
            //需要加密的内容长度
            int dataLength = data.Length;

            string str = string.Empty;
            for (int i = 0; i < dataLength; i++)
            {
                if (index == sha256Key.Length)
                    index = 0;
                //当前遍历到的元素与加密key中的index位置的元素相加作为新的值
                str += (char)((short)(Convert.ToChar(data.Substring(i, 1))) + (short)(Convert.ToChar(sha256Key.Substring(index, 1))) % 256);
                index++;
            }
            //转换成Base64返回
            byte[] bytes = System.Text.Encoding.GetEncoding("utf-8").GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 解密
        /// </summary>
        /// <param name="data">需要解密是数据</param>
        /// <param name="key">解密key</param>
        /// <returns></returns>
        public static string Decrypt(string data, string key)
        {
            //从Base64解码
            byte[] bytes = Convert.FromBase64String(data);
            data = System.Text.Encoding.GetEncoding("utf-8").GetString(bytes);

            //对key做sha256加密
            byte[] sha256Data = System.Text.Encoding.UTF8.GetBytes(key);
            System.Security.Cryptography.SHA256Managed sha256 = new System.Security.Cryptography.SHA256Managed();
            string sha256Key = Convert.ToBase64String(sha256.ComputeHash(sha256Data));
            //当前取加密key中的某个元素索引
            int index = 0;
            //需要加密的内容长度
            int dataLength = data.Length;
            string str = string.Empty;
            for (int i = 0; i < dataLength; i++)
            {
                if (index == sha256Key.Length)
                    index = 0;
                if ((short)(Convert.ToChar(data.Substring(i, 1))) < (short)(Convert.ToChar(sha256Key.Substring(index, 1))))
                {
                    str += (char)(((short)(Convert.ToChar(data.Substring(i, 1))) + 256) - (short)(Convert.ToChar(sha256Key.Substring(index, 1))));
                }
                else
                {
                    str += (char)((short)(Convert.ToChar(data.Substring(i, 1))) - (short)(Convert.ToChar(sha256Key.Substring(index, 1))));
                }
                index++;
            }
            return str;
        }
    }
}