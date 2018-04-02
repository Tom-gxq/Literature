using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.Entities
{
    public class SupportRange
    {
        /// <summary>
        /// 中国
        /// </summary>
        public SupportInfo China { get; set; }

        /// <summary>
        /// 国际
        /// </summary>
        public SupportInfo International { get; set; }
    }
}
