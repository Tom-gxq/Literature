using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class BindedModel
    {
        public string bindedWeiXin { get; set; }
        public bool IsBindedWeiXin { get; set; }
        public string bindedAliPay { get; set; }
        public bool IsBindedAliPay { get; set; }
        public string bindedQQ { get; set; }
        public bool IsBindedQQ { get; set; }
    }
}
