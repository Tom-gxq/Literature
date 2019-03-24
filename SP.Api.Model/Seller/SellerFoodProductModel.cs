using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Seller
{
    public class SellerFoodProductModel
    {
        public int SuppliersId { get; set; }
        public string ProductName { get; set; }
        public double PurchasePrice { get; set; }
        public string ImagePath { get; set; }
        public string ProductId { get; set; }
        public int SupplierProductId { get; set; }
        public int SelectedStatus { get; set; }
    }
}
