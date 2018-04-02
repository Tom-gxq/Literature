using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPManager.Common
{
    public class Function
    {
        public static bool IsAllowed(string controller, string action)
        {
            if((controller.Equals("Home", StringComparison.OrdinalIgnoreCase)
                && action.Equals("Index", StringComparison.OrdinalIgnoreCase))
                ||(controller.Equals("Admin", StringComparison.OrdinalIgnoreCase)
                && action.Equals("CheckAdminLogin", StringComparison.OrdinalIgnoreCase)))
            {
                return true;
            }            
            else
            {
                return false;
            }
        }
    }
}