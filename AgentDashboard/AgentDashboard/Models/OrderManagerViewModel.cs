using SP.Application.Product.DTO;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgentDashboard.Models
{
    public class OrderManagerViewModel
    {
        public Dictionary<int, String> Universities { get; set; }

    }

    public class OrderManagerModel
    {
        public string OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int OrderStatus { get; set; }
        public string OrderCode { get; set; }
        public string OrderAddress { get; set; }
        public bool? IsVip { get; set; }
        public decimal? Amount { get; set; }
        public bool? IsWxPay { get; set; }
        public bool? IsAliPay { get; set; }
        public string Remark { get; set; }
        public string AccountId { get; set; }
        public List<ProductsDto> ProductList { get; set; }
        public AccountInfoDto Owner { get; set; }
        public List<AccountInfoDto> Shiper { get; set; }
    }
}
