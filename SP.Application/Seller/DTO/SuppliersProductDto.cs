using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Seller.DTO
{
    public class SuppliersProductDto
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string ProductId { get; set; }
        public float PurchasePrice { get; set; }
        public int Status { get; set; }
        public int AlertStock { get; set; }
    }
}
