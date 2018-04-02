using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_ShoppingCarts")]
    public class ShoppingCartsEntity : Entity
    {
        [AutoIncrement]
        public int? Id { get; set; }

        public string CartId { get; set; }

        public string ProductId { get; set; }
        public string AccountId { get; set; }
        public int? Quantity { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public string OrderId { get; set; }
        public int? ShopId { get; set; }
        public bool? IsEnabled { get; set; }
    }
}
