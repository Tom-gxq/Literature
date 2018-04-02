using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApiGateway.App_Start.Crypt
{
    public interface ICrypt
    {
        string encrypt(string str);

        string decrypt(string str);
    }
}
