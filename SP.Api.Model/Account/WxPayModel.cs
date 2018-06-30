using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model
{
    public class WxPayModel
    {
        public int retcode { get; set; }
        public string retmsg { get; set; }
        public string appid { get; set; }
        public string partnerid { get; set; }
        public string prepayid { get; set; }
        public string package { get; set; }
        public string noncestr { get; set; }
        public string timestamp { get; set; }
        public string sign { get; set; }
    }
}
