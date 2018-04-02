using LibMain.Dependency;
using SP.Application.User;
using SP.Application.User.DTO;
using SPManager.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPManager.Filters
{
    public class UserActionAttribute : ActionFilterAttribute
    {
        private AdminDto CurrentUser
        {
            get
            {
                IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
                var result = service.GetCurrentSession();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }
            }
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = CurrentUser;
            var controller = filterContext.RouteData.Values["controller"].ToString();
            var action = filterContext.RouteData.Values["action"].ToString();
            var isAllowed = Function.IsAllowed(controller, action);
            if (isAllowed)
            {
                
                base.OnActionExecuting(filterContext);
            }            
            else
            {
                if (user != null)
                {
                    base.OnActionExecuting(filterContext);
                }
                else
                {
                    filterContext.Result = new RedirectResult("/Home/Index");
                }
            }
        }


    }
}