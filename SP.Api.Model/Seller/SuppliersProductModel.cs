using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Seller
{
    public class SuppliersProductModel
    {
        public int SuppliersId { get; set; }
        public string ProductName { get; set; }
        public double PurchasePrice { get; set; }
        public string ProductId { get; set; }
        public int SaleStatus { get; set; }
        public string ImagePath { get; set; }
    }
}
