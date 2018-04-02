using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Shop.DTO
{
    public class ShopDto
    {
        public int Id { get; set; }
        public string ShopName { get; set; }
        public int DisplaySequence { get; set; }
        public string OwnerId { get; set; }
        public long AttributeId { get; set; }
        public int RegionId { get; set; }
        public string RegionName { get; set; }
        public int CityId { get; set; }
        public int SchoolId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public AccountInfoDto Owner { get; set; }
}
}
