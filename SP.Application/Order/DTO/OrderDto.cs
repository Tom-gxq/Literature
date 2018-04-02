using SP.Application.Product.DTO;
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
        public List<ProductsDto> ProductList { get; set; }
    }
}
