using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Account
{
    public class AddressModel
    {
        public int id { get; set; }
        public string contactName { get; set; }
        public string contactAddress{ get; set; }
        public string contactMobile { get; set; }
        public bool gender { get; set; }
        public int status { get; set; }
        public string accountId { get; set; }
        public int districtId { get; set; }
        public string districtName { get; set; }
        public int schoolId { get; set; }
        public string schoolName { get; set; }
        public string dorm { get; set; }
        public bool isDefault { get; set; }
    }
}
