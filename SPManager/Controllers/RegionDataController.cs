using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SPManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SPManager.Controllers
{
    public class RegionDataController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "学区管理";
            
            return View();
        }
        public JsonResult GetRegionData(int dataType,int pageIndex,int pageSize)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.GetRegionData(dataType);
            var total = service.GetRegionDataCount(dataType);
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SearchRegionData(string dataName)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.SearchRegionData(dataName);
            JsonResult.Add("items", list);
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetChildRegionData(int parentId, int pageIndex, int pageSize)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.GetChildRegionData(parentId, pageIndex, pageSize);
            JsonResult.Add("items", list);
            var total = service.GetChildRegionDataCount(parentId);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        /// <summary>
        /// 店铺获取级联菜单用
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        public JsonResult GetChildRegionDataBorShopMenu(int parentId)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.GetChildRegionData(parentId);
            JsonResult.Add("items", list);            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }


        public JsonResult AddRegionData([FromBody]RegionDto region)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.AddRegionData(region);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DelRegionData(int dataId)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.DelRegionData(dataId);            
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetRegionDataDetail(int dataId)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.GetRegionDataDetail(dataId);

           
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult EditRegionData([FromBody]RegionDto region)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.EditRegionData(region);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GenderBuildingNum(int parentId,int start,int end)
        {
            bool reuslt = true;
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            try
            {
                for (int i = start; i <= end; i++)
                {
                    var dataName = $"{i}栋";
                    var dto = service.GetRegionData(parentId, dataName);
                    if (dto == null)
                    {
                        var region = new RegionDto()
                        {
                            ParentDataID = parentId,
                            DataName = dataName,
                            DataType = 3,
                            Status = 1,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        var result = service.AddRegionData(region);
                    }
                }
            }
            catch(Exception e)
            {
                reuslt = false;
            }
            
            JsonResult.Add("result", reuslt);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GenderDormNum(int parentId, int start, int end)
        {
            bool reuslt = true;
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            try
            {
                for (int i = start; i <= end; i++)
                {
                    var dataName = $"{i}";
                    var dto = service.GetRegionData(parentId, dataName);
                    if (dto == null)
                    {
                        var region = new RegionDto()
                        {
                            ParentDataID = parentId,
                            DataName = dataName,
                            DataType = 4,
                            Status = 1,
                            CreateTime = DateTime.Now,
                            UpdateTime = DateTime.Now
                        };
                        var result = service.AddRegionData(region);
                    }
                }
            }
            catch (Exception e)
            {
                reuslt = false;
            }

            JsonResult.Add("result", reuslt);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public ActionResult RegionTypeIndex()
        {
            return View();
        }
        public JsonResult GetRegionType(int pageIndex, int pageSize)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.GetRegionTypeList(pageIndex, pageSize);
            var total = service.GetRegionTypeCount();
            JsonResult.Add("items", list);
            PageModel jObject = new PageModel();
            jObject.Total = (int)total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddRegionType([FromBody]RegionTypeDto region)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.AddRegionType(region);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DelRegionType(int id)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.DelRegionType(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchRegionTypeByKeyWord(string keywords)
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var result = service.SearchRegionTypeByKeyWord(keywords);
            JsonResult.Add("items", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}