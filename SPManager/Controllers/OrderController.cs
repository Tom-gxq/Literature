using LibMain.Dependency;
using SP.Application.Order;
using SP.Application.Product;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class OrderController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "订单管理";
            return View();
        }
        public JsonResult GetOrderList(int status, int pageIndex, int pageSize)
        {
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var list = service.GetOrderList(status, pageIndex, pageSize);
            var total = service.GetOrderListCount(status);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);

            IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
            foreach(var item in list)
            {
                item.ProductList = productService.GetProductListByOrderId(item.OrderId);
                foreach (var pitem in item.ProductList)
                {
                    string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                    foreach (var pimg in pitem.ProductImage)
                    {
                        if (!string.IsNullOrEmpty(pimg.ImgPath) && !string.IsNullOrEmpty(domain))
                        {
                            pimg.ImgPath = domain + pimg.ImgPath;
                        }
                    }
                }
            }
            JsonResult.Add("result", list);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateOrderStatus(string orderId, int status)
        {
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var result = service.UpdateOrderStatus(orderId, status);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteOrderById(string orderId)
        {
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var result = service.DeleteOrderById(orderId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchOrderListByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            IOrderAppService service = IocManager.Instance.Resolve<IOrderAppService>();
            var list = service.SearchOrderListByKeyWord(keyWord, pageIndex, pageSize);
            IProductAppService productService = IocManager.Instance.Resolve<IProductAppService>();
            foreach (var item in list)
            {
                item.ProductList = productService.GetProductListByOrderId(item.OrderId);
                foreach (var pitem in item.ProductList)
                {
                    string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                    foreach (var pimg in pitem.ProductImage)
                    {
                        if (!string.IsNullOrEmpty(pimg.ImgPath) && !string.IsNullOrEmpty(domain))
                        {
                            pimg.ImgPath = domain + pimg.ImgPath;
                        }
                    }
                }
            }

            JsonResult.Add("result", list);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult SumIndex()
        {
            ViewBag.Title = "订单统计管理";
            return View();
        }
    }
}
