using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class ShopModel
    {
        /**
    * 店铺ID
    */
        public long shopId { get; set; }
        /**
        * 店铺名
        */
        public string shopName { get; set; }
        /**
        * 店铺拥有者ID
        */
        public string ownerId { get; set; }
        /**
        * 店铺拥有者名
        */
        public string ownerName { get; set; }
        public string startTime { get; set; }
        public string endTime { get; set; }
        public string shopLogo { get; set; }
    }
}
