using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.ValueException
{
    public class ValueFormatException: ApplicationException
    {
        public string FormatContent { get; set; }
         /// <summary>
        /// 默认构造函数
        /// </summary>
        public ValueFormatException() { }
        public ValueFormatException(string message)
            : base(message) { }
        public ValueFormatException(string message, Exception inner)
            : base(message, inner) { }
        public ValueFormatException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
