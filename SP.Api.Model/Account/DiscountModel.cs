using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class DiscountModel
    { 
        /**
        *  订购的数量
        */
        public int Quantity { get; set; }
        /**
        *  已支付的金额
        */
        public double Amount { get; set; }
        
        /**
        *  创建时间
        */
        public string StartDate { get; set; }
        /**
        *  更新时间
        */
        public string EndDate { get; set; }
        /**
        *  描述
        */
        public string Description { get; set; }
        /**
        *  优惠的钱
        */
        public double DiscountValue { get; set; }
    }
}
