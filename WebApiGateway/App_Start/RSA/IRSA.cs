using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGateway.App_Start.RSA
{
    interface IRSA
    {
        string RSAEncrypt(string strEncryptString, string publickey);

        string RSADecrypt(string xmlPrivateKey, string strDecryptString);
    }
}
