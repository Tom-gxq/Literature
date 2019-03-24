using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Seller
{
    public class AddSuppliersProductModel
    {
        public int SuppliersId { get; set; }
        public double PurchasePrice { get; set; }
        public string ProductId { get; set; }
        public int mainType { get; set; }
        public int secondType { get; set; }
    }
}
