using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Seller
{
    public class SupplierModel
    {
        public int SuppliersId { get; set; }
        public string SuppliersName { get; set; }
        public string AccountId { get; set; }
        public string AlipayNo { get; set; }
        public string CellPhone { get; set; }
        public int TypeId { get; set; }
    }
}
