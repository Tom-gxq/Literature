using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class AccountProductDto
    {
        public string AccountId { get; set; }
        public string ProductId { get; set; }
        public int ShopId { get; set; }
        public int PreStock { get; set; }
        public int Status { get; set; }
    }
}
