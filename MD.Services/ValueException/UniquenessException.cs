using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MD.Services.ValueException
{
    public class UniquenessException: ApplicationException
    {
         /// <summary>
        /// 默认构造函数
        /// </summary>
        public UniquenessException() { }
        public UniquenessException(string message)
            : base(message) { }
        public UniquenessException(string message, Exception inner)
            : base(message, inner) { }
        public UniquenessException(System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}
