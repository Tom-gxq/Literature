using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Sms.Service.Utility
{
    public class UtilHelper
    {
        #region 时间戳

        public static long GetTimestamp(DateTime time)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0);
            return (long)(time.ToUniversalTime() - startTime).TotalSeconds;
        }

        #endregion

        #region 手机号码

        /// <summary>
        /// 不带+86
        /// </summary>
        public static string GetChineseMobileNumberWithoutCode(string mobilePhone)
        {
            if (ValidateChineseMobileNumber(mobilePhone))
            {
                return mobilePhone.Substring(mobilePhone.Length - 11);
            }
            return mobilePhone;
        }

        /// <summary>
        /// 判断是否是手机号码
        /// </summary>
        public static bool ValidateChineseMobileNumber(string mobileNumber)
        {
            return Regex.IsMatch(mobileNumber, @"^(\((\+)?86\)|((\+)?86)?)0?1[3-8]{1}\d{9}$");
        }

        #endregion
    }
}
