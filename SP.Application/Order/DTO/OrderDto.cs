using SP.Application.Product.DTO;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Order.DTO
{
    public class OrderDto
    {
        public string OrderId { get; set; }
        public string OrderCode { get; set; }
        public string Remark { get; set; }
        public int OrderStatus { get; set; }
        public string CloseReason { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime PayDate { get; set; }
        public DateTime ShippingDate { get; set; }
        public DateTime FinishDate { get; set; }
        public string AccountId { get; set; }
        public DateTime ShipToDate { get; set; }
        public decimal Freight { get; set; }
        public decimal Amount { get; set; }
        public string OrderAddress { get; set; }
        public int ShopId { get; set; }
        public bool IsVip { get; set; }
        public bool IsWxPay { get; set; }
        public bool IsAliPay { get; set; }
        public string ShiperId { get; set; }
        public List<ProductsDto> ProductList { get; set; }
        public AccountInfoDto Owner { get; set; }
        public List<AccountInfoDto> Shiper { get; set; }
    }
}
