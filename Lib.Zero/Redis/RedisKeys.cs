using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Zero.Redis
{
    class RedisKeys
    {
        //Session
        public const string SessionHashKey = "H:A:S:I:"; // session key
        public const string SessionAccountHashKey = "H:A:SA:l:"; // key accountid, field account,value sessionid
    }
}
