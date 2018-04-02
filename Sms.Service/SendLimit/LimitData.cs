using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.SendLimit
{
    public class LimitData
    {
        public string Key { get; set; }

        public int Count { get; set; } = 1;

        public DateTime ExpiryDate { get; set; }

        public int LimitCount { get; set; }
    }
}
