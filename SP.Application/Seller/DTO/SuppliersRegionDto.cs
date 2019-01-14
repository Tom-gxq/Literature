using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Seller.DTO
{
    public class SuppliersRegionDto
    {
        public int Id { get; set; }
        public int SuppliersId { get; set; }
        public string SuppliersName { get; set; }
        public int RegionID { get; set; }
        public string RegionName { get; set; }
    }
}
