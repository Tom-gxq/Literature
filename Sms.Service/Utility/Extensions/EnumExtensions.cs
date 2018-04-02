using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text;

namespace Sms.Service.Utility.Extensions
{
    public static class EnumExtensions
    {
        public static string GetEnumDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false) as DisplayAttribute[];

            if (descriptionAttributes != null && descriptionAttributes.Length > 0)
                return descriptionAttributes[0].Description;

            return string.Empty;
        }
    }
}
