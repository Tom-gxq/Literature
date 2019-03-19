using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class TradeInfoModel
    {
        /**
        * 交易类型
        */
        public int Type { get; set; }
        /**
        * 交易时间
        */
        public long CeateTime { get; set; }
        /**
        * 交易单号
        */
        public string TradeNo { get; set; }
        /**
        *  零钱余额
        */
        public double BalanceAmount { get; set; }
        /**
        *  消费的余额
        */
        public double Amount { get; set; }
    }
}
