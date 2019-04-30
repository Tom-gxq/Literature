using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WXApiGateway.Common
{
    public class BaseGetResponse<T>
    {
        public ResultCode Code { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
    }
    public enum ResultCode
    {
        NormalCode,
        NotExistsValue
    }
}
