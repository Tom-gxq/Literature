using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class SellerViewModel
    {
        public int SellerId { get; set; }
        public string AccountId { get; set; }
        public string SellerName { get; set; }
        public string LogoPath { get; set; }
        public string TelNumber { get; set; }
        /// <summary>
        /// 营业执照
        /// </summary>
        public string LicensePath { get; set; }
        /// <summary>
        /// 经营许可
        /// </summary>
        public string PermitPath { get; set; }
        /// <summary>
        /// 授权函
        /// </summary>
        public string AuthorizationPath { get; set; }
        /// <summary>
        /// 支付宝提现账号
        /// </summary>
        public string AlipayNo { get; set; }
    }
}