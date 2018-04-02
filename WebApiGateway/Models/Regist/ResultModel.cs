using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApiGateway.Models.Regist
{
    [Serializable]
    public class ResultModel
    {
        public ApiEnum.ErrorCode code = ApiEnum.ErrorCode.Success;
        public string error_msg = string.Empty;
        public string access_token = string.Empty;




        internal static ResultModel GetFailedInstance(string errorMsg, ApiEnum.ErrorCode errorCode)
        {
            return new ResultModel()
            {
                error_msg = errorMsg,
                code = errorCode
            };
        }

        internal static ResultModel GetFailedInstance(string errorMsg)
        {
            return GetFailedInstance(errorMsg, ApiEnum.ErrorCode.Fail);
        }

        internal static ResultModel GetFailedInstance()
        {
            return GetFailedInstance("开发者比较懒，什么描述都没有填", ApiEnum.ErrorCode.Fail);
        }
    }
}