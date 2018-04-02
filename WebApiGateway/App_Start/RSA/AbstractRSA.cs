using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebApiGateway.App_Start.RSA
{
    class AbstractRSA : IRSA
    {
        private readonly string mdpublickey =
        @"-----BEGIN PUBLIC KEY-----
        MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQC1xzCYtdu8bZEinh6Oh7/p+6xc
        ilHgV/ChU3bZXyezLQqf6mzOnLH6GVZMMDafMw3uMtljWyECCqnECy2UhZPa5BFc
        qA2xbYH8/WyKTraCRJT3Hn61UrI4Eac4YVxa1CJ8KaTQtIeZBoXHIW0r5XyhBwYe
        NkSun+OFN+YBoJvCXwIDAQAB
        -----END PUBLIC KEY-----";

        private readonly string mdprivtekey =
        @"-----BEGIN RSA PRIVATE KEY-----
        MIICXQIBAAKBgQC1xzCYtdu8bZEinh6Oh7/p+6xcilHgV/ChU3bZXyezLQqf6mzO
        nLH6GVZMMDafMw3uMtljWyECCqnECy2UhZPa5BFcqA2xbYH8/WyKTraCRJT3Hn61
        UrI4Eac4YVxa1CJ8KaTQtIeZBoXHIW0r5XyhBwYeNkSun+OFN+YBoJvCXwIDAQAB
        AoGAN1Fu0IpHXIhbapWD5wwYszQLt/2//O3GJNIpkO0MP9KtMQ0+H4JAB0Q+puDl
        Pn1i9+IxlbLd0Kk+EJL2RASCgc8FmYCTY/pgwc2kw1qh74UeI/Ok1YoDJnVhbyYB
        j/FsWPl/B8gQI3rF/qJCcsoKHRm4bz14+7t3jzIGOcQyjMECQQDiBmwQsKHqfR7g
        bpfDkP6/lsz1oQKLSSbs/FUidGdlbtuONgj1quC4jOPwJ9Yis4+pk/cVbdTNqdU2
        tLyaDa17AkEAzeKTrral+kf4utdS+ZIG3na0w8+3uQZ7HCVV9HLPjmCFuw94RuyX
        TfQLCC6ThWsEYI7knRaatsP5M1ZKiiUfbQJAKXDj/2tjRIsMTjn4uXKsQpRzn9WV
        kdQnvuvE8DxHeOGKf9iIbAKYkT3DzRSAvnwNqxnmA5fPnKW24gDhU52OYQJBALl0
        sYcdq+EJV7omH+4DZgCaeTYxM9ONTPQLhaPOj7w2of/gbX2lvJ1RiWZzXhs+TREV
        ZkVCiVa8rQtbXYWW7vkCQQCPq3bdhx+x9iYB1c7+3cds/gbsKL1YAVUf1anfrrkM
        cZu8TxTBHjSTGtDfAYL9l0nrt88wFYfwiUQVJNbqet5o
        -----END RSA PRIVATE KEY-----";

        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="strEncryptString">加密数据</param>
        /// <returns>RSA公钥加密后的数据</returns>
        public string RSAEncrypt(string strEncryptString, string publickey)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = new RSACryptoServiceProvider();
                provider.ImportParameters(ConvertFromPemPublicKey(!string.IsNullOrEmpty(publickey) ? publickey : mdpublickey));
                //byte[] bytes = Encoding.Unicode.GetBytes(strEncryptString);
                byte[] bytes = Encoding.UTF8.GetBytes(strEncryptString);
                str2 = Convert.ToBase64String(provider.Encrypt(bytes, false));
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="privateKey">私钥</param>
        /// <param name="strDecryptString">待解密的数据</param>
        /// <returns>解密后的结果</returns>
        public string RSADecrypt(string privateKey, string strDecryptString)
        {
            string str2;
            try
            {
                RSACryptoServiceProvider provider = DecodeRSAPrivateKey(!string.IsNullOrEmpty(privateKey) ? privateKey : mdprivtekey);

                byte[] rgb = Convert.FromBase64String(strDecryptString);

                byte[] buffer2 = provider.Decrypt(rgb, false);
                //str2 = new UnicodeEncoding().GetString(buffer2);
                //str2 = System.Text.Encoding.UTF8.GetString(buffer2);
                str2 = Encoding.UTF8.GetString(buffer2);
                str2 = str2.Replace("+", "%2B");
                str2 = System.Web.HttpUtility.UrlDecode(str2);//IOS中文字符URL编码
            }
            catch (Exception exception)
            {
                throw exception;
            }
            return str2;
        }

        #region 转格式
        /// <summary>
        /// 将pem格式公钥转换为RSAParameters
        /// </summary>
        /// <param name="pemFileConent">pem公钥内容</param>
        /// <returns>转换得到的RSAParamenters</returns>
        public static RSAParameters ConvertFromPemPublicKey(string pemFileConent)
        {
            if (string.IsNullOrEmpty(pemFileConent))
            {
                throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");
            }
            pemFileConent = pemFileConent.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");
            byte[] keyData = Convert.FromBase64String(pemFileConent);
            if (keyData.Length < 162)
            {
                throw new ArgumentException("pem file content is incorrect.");
            }
            byte[] pemModulus = new byte[128];
            byte[] pemPublicExponent = new byte[3];
            Array.Copy(keyData, 29, pemModulus, 0, 128);
            Array.Copy(keyData, 159, pemPublicExponent, 0, 3);
            RSAParameters para = new RSAParameters();
            para.Modulus = pemModulus;
            para.Exponent = pemPublicExponent;
            return para;
        }

        /// <summary>
        /// 将pem私钥转为RSAParameters
        /// </summary>
        /// <param name="privateKey"></param>
        /// <returns></returns>
        private static RSACryptoServiceProvider DecodeRSAPrivateKey(string privateKey)
        {
            if (string.IsNullOrEmpty(privateKey))
            {
                throw new ArgumentNullException("pemFileConent", "This arg cann't be empty.");
            }
            privateKey = privateKey.Replace("-----BEGIN RSA PRIVATE KEY-----", "").Replace("-----END RSA PRIVATE KEY-----", "").Replace("\n", "").Replace("\r", "");

            var privateKeyBits = System.Convert.FromBase64String(privateKey);

            var RSA = new RSACryptoServiceProvider();
            var RSAparams = new RSAParameters();

            using (BinaryReader binr = new BinaryReader(new MemoryStream(privateKeyBits)))
            {
                byte bt = 0;
                ushort twobytes = 0;
                twobytes = binr.ReadUInt16();
                if (twobytes == 0x8130)
                    binr.ReadByte();
                else if (twobytes == 0x8230)
                    binr.ReadInt16();
                else
                    throw new Exception("Unexpected value read binr.ReadUInt16()");

                twobytes = binr.ReadUInt16();
                if (twobytes != 0x0102)
                    throw new Exception("Unexpected version");

                bt = binr.ReadByte();
                if (bt != 0x00)
                    throw new Exception("Unexpected value read binr.ReadByte()");

                RSAparams.Modulus = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Exponent = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.D = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.P = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.Q = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DP = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.DQ = binr.ReadBytes(GetIntegerSize(binr));
                RSAparams.InverseQ = binr.ReadBytes(GetIntegerSize(binr));
            }

            RSA.ImportParameters(RSAparams);
            return RSA;
        }

        private static int GetIntegerSize(BinaryReader binr)
        {
            byte bt = 0;
            byte lowbyte = 0x00;
            byte highbyte = 0x00;
            int count = 0;
            bt = binr.ReadByte();
            if (bt != 0x02)
                return 0;
            bt = binr.ReadByte();

            if (bt == 0x81)
                count = binr.ReadByte();
            else
                if (bt == 0x82)
            {
                highbyte = binr.ReadByte();
                lowbyte = binr.ReadByte();
                byte[] modint = { lowbyte, highbyte, 0x00, 0x00 };
                count = BitConverter.ToInt32(modint, 0);
            }
            else
            {
                count = bt;
            }

            while (binr.ReadByte() == 0x00)
            {
                count -= 1;
            }
            binr.BaseStream.Seek(-1, SeekOrigin.Current);
            return count;
        }
        #endregion
    }
}