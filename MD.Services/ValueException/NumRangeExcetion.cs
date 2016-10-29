using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.ValueException
{
    public class NumRangeExcetion : ApplicationException
    {
        public string NumRange { get; set; }
         /// <summary>
        /// 默认构造函数
        /// </summary>
        public NumRangeExcetion() { }
        public NumRangeExcetion(string message)
            : base(message) { }
        public NumRangeExcetion(string message, Exception inner)
            : base(message, inner) { }
        public NumRangeExcetion(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
