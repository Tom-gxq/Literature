using SP.Api.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace WebApiGateway.App_Start.Crypt
{
    public class StringCrypt : AbstractCrypt
    {
        /// <summary>
        /// 返回加密
        /// </summary>
        public override SymmetricAlgorithm getSymmetricAlgorithm()
        {
            return new System.Security.Cryptography.RijndaelManaged();
        }

        /// <summary>
        /// 加密
        /// </summary>
        public static string Encrypt(string str, string useKey)
        {
            if (!string.IsNullOrEmpty(str) && useKey == ConfigInfo.ConfigInfoData.CryptKey.MessageKey)
            {
                ICrypt crypt = new StringCrypt();
                return crypt.encrypt(str);
            }

            return string.Empty;
        }

        /// <summary>
        /// 解密
        /// </summary>
        public static string Decrypt(string str, string useKey)
        {
            if (!string.IsNullOrEmpty(str) && useKey == ConfigInfo.ConfigInfoData.CryptKey.MessageKey)
            {
                ICrypt crypt = new StringCrypt();
                return crypt.decrypt(str);
            }

            return string.Empty;
        }
    }
}