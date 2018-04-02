using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Order
{
    public class TradeModel
    {
        /**
        * 负责人
        */
        public string accountId { get; set; }
        /**
        * 提成金额
        */
        public double amount { get; set; }
        /**
        * 购物车ID
        */
        public string cartId { get; set; }
        /**
        * 日期
        */
        public string createTime { get; set; }

        /**
        * 购买数量
        */
        public int quantity { get; set; }

        /**
        * 内容
        */
        public string title { get; set; }
    }
}
