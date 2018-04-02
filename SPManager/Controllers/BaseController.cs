using SP.Application.User.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class BaseController : Controller
    {
        protected Dictionary<string, object> JsonResult = new Dictionary<string, object>();
        
    }
}