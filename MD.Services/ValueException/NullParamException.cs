using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.ValueException
{
    public class NullParamException : ApplicationException
    {
        /// <summary>
        /// 默认构造函数
        /// </summary>
        public NullParamException() { }
        public NullParamException(string message)
            : base(message) { }
        public NullParamException(string message, Exception inner)
            : base(message, inner) { }
        public NullParamException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
