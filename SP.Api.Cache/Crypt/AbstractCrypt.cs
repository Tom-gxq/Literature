using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApiGateway.App_Start.Crypt
{
    public abstract class AbstractCrypt : ICrypt
    {
        private static string defaultKey = "123456789";
        private static string defaultIV = "MingDao";

        /// <summary>
        /// 加密算法
        /// </summary>
        public string encrypt(string str)
        {
            SymmetricAlgorithm mobjCryptoService = getSymmetricAlgorithm();
            byte[] bytIn = UTF8Encoding.UTF8.GetBytes(str);
            MemoryStream ms = new MemoryStream();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateEncryptor();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            cs.Close();
            ms.Close();
            byte[] bytOut = ms.ToArray();

            return Convert.ToBase64String(bytOut);
        }

        /// <summary>
        /// 解密算法
        /// </summary>
        public string decrypt(string str)
        {
            SymmetricAlgorithm mobjCryptoService = getSymmetricAlgorithm();
            mobjCryptoService.Key = GetLegalKey();
            mobjCryptoService.IV = GetLegalIV();
            ICryptoTransform encrypto = mobjCryptoService.CreateDecryptor();
            byte[] bytIn = Convert.FromBase64String(str);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
            cs.Write(bytIn, 0, bytIn.Length);
            cs.FlushFinalBlock();
            cs.Close();
            ms.Close();
            byte[] bytOut = ms.ToArray();

            return Encoding.UTF8.GetString(bytOut);
        }

        /// <summary>
        /// 获得密钥
        /// </summary>
        private byte[] GetLegalKey()
        {
            string sTemp = defaultKey;
            SymmetricAlgorithm mobjCryptoService = getSymmetricAlgorithm();
            mobjCryptoService.GenerateKey();
            byte[] bytTemp = mobjCryptoService.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        /// <summary>
        /// 获得初始向量IV  
        /// </summary>
        private byte[] GetLegalIV()
        {
            string sTemp = defaultIV;
            SymmetricAlgorithm mobjCryptoService = getSymmetricAlgorithm();
            mobjCryptoService.GenerateIV();
            byte[] bytTemp = mobjCryptoService.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');

            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 返回对称算法实现类
        /// </summary>
        public abstract SymmetricAlgorithm getSymmetricAlgorithm();
    }
}