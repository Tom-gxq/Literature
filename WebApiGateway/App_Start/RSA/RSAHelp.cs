using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.App_Start.RSA
{
    public class RSAHelp
    {
        static IRSA IRSA;

        static RSAHelp()
        {
            IRSA = new AbstractRSA();
        }

        public static string RSAEncrypt(string strEncryptString, string publickey = "")
        {
            return IRSA.RSAEncrypt(strEncryptString, publickey);
        }

        public static string RSADecrypt(string xmlPrivateKey, string strDecryptString)
        {
            return IRSA.RSADecrypt(xmlPrivateKey, strDecryptString);
        }
    }
}