using LibMain.Dependency;
using SP.Application.User;
using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;


public class GlobalSession
{
    public static AdminDto CurrentUser
    {
        get
        {
            IAdminAppService service = IocManager.Instance.Resolve<IAdminAppService>();
            var result = service.GetCurrentSession();
            return result;
        }
    }
}