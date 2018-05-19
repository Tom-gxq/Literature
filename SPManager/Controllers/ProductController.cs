using LibMain.Dependency;
using SP.Application.Product;
using SP.Application.Product.DTO;
using SP.Application.Shop;
using SP.Application.User;
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
    public class ProductController : BaseController
    {
        public ActionResult Index()
        {
            ViewBag.Title = "商品管理";
            IProductTypeService service = IocManager.Instance.Resolve<IProductTypeService>();
            var list = service.GetAllProductTypeList(0);
            ViewBag.TypeData = list;

            IBrandAppService brandService = IocManager.Instance.Resolve<IBrandAppService>();
            var brandList = brandService.GetAllBrandList();
            ViewBag.BrandData = brandList;
            return View();
        }
        public JsonResult AddProduct([FromBody]ProductsDto product)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddProduct(product);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductList(int saleStatus, int pageIndex, int pageSize)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.GetProductList(saleStatus, pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetProductListCount(saleStatus);
            PageModel jObject = new PageModel();
            jObject.Total = total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetShopProductList(int shopId,int saleStatus, int pageIndex, int pageSize)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.GetShopProductList(shopId,saleStatus, pageIndex, pageSize);
            JsonResult.Add("result", result);
            var total = service.GetShopProductListCount(shopId,saleStatus);
            PageModel jObject = new PageModel();
            jObject.Total = total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult GetAllShopProductList(int shopId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.GetShopProductList(shopId, 1, 1, 100000);
            JsonResult.Add("items", result);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddProductBrand(string productId, int brandId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddProductBrand(productId, brandId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddProductType(string productId, int typeId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddProductBrand(productId, typeId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddProductImage([FromBody]ProductImageDto image)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddProductImage(image);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateProductImageDisplaySequence(int id, int sequence)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.UpdateProductImageDisplaySequence(id, sequence);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteProductImageById(int id)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DeleteProductImageById(id);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult AddProductAttribute([FromBody]ProductAttributeDto attribute)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddProductAttribute(attribute);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteProductAttribute(string productId, long attributeId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DeleteProductAttribute(productId, attributeId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteProductImage(string productId, long imageId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DeleteProductImage(productId, imageId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SetProductAttributeValue(string productId, long attributeId, long valueId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.SetProductAttributeValue(productId, attributeId, valueId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchProductList(string keyWord, int typeId, int brandId, int saleStatus, int pageIndex, int pageSize)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.SearchProductList(keyWord, typeId, brandId, saleStatus, pageIndex, pageSize);
            JsonResult.Add("result", result);
            PageModel jObject = new PageModel();
            var total = service.SearchProductListCount(keyWord, typeId, brandId, saleStatus);
            jObject.Total = total;
            jObject.Pages = (int)Math.Ceiling(Convert.ToDouble(total) / pageSize);
            jObject.Index = pageIndex;
            JsonResult.Add("data", jObject);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult GetProductDetail(string productId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.GetProductDetail(productId);
            JsonResult.Add("result", result);
            IBrandAppService brandService = IocManager.Instance.Resolve<IBrandAppService>();
            var brandList = brandService.GetAllBrandList();
            JsonResult.Add("brands", brandList);
            IProductTypeService typeService = IocManager.Instance.Resolve<IProductTypeService>();
            var typeList = typeService.GetAllProductTypeList(0);
            JsonResult.Add("types", typeList);
            var stypeList = typeService.GetAllProductTypeList(1);
            JsonResult.Add("secondTypes", stypeList);
            IAttributeAppService attrService = IocManager.Instance.Resolve<IAttributeAppService>();
            var attrList = attrService.GetAttributeList(1,1000);
            JsonResult.Add("attributes", attrList);
            var productAttribute = service.GetAttributeList(productId);
            JsonResult.Add("productAttribute", productAttribute);
            var productImage = service.GetImageList(productId);
            if(productImage != null)
            {
                string domain = ConfigurationManager.AppSettings["Qiniu.Domain"];
                foreach (var item in productImage)
                {
                    if(!string.IsNullOrEmpty(item.ImgPath) && !string.IsNullOrEmpty(domain))
                    {
                        item.ImgPath = domain+item.ImgPath;
                    }
                }
            }
            JsonResult.Add("productImage", productImage);
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult UpdateProductSaleStatus(string productId, int saleStatus)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.UpdateProductSaleStatus(productId, saleStatus);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult UpdateManyProductSaleStatus(string productIds, int saleStatus)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var ids = productIds.Split(',');
            int sucessCount = 0;
            foreach (var idItem in ids)
            {
                var result = service.UpdateProductSaleStatus(idItem, saleStatus);
                if(result)
                {
                    sucessCount++;
                }
            }
            JsonResult.Add("SubmitCont", ids.Count());
            JsonResult.Add("SucessCount", sucessCount);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult DeleteProduct(string productId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DeleteProduct(productId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult EditProduct([FromBody]ProductsDto product)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.EditProduct(product);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public ActionResult SkuIndex()
        {
            IRegionAppService service = IocManager.Instance.Resolve<IRegionAppService>();
            var list = service.GetRegionData(1);
            ViewBag.SchoolData = list;
            
            var districtList = service.GetRegionData(2);
            ViewBag.DistrictData = districtList;

            IShopAppService brandService = IocManager.Instance.Resolve<IShopAppService>();
            var shopList = brandService.GetShopList(1,100000);
            ViewBag.ShopData = shopList;

            IProductAppService pService = IocManager.Instance.Resolve<IProductAppService>();
            var pList = pService.GetProductList(1,1,100000);
            ViewBag.ProductData = pList;
            return View();
        }
        public JsonResult GetProducSkuList(int pageIndex, int pageSize)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var list = service.GetProducSkuList(pageIndex, pageSize);
            var total = service.GetProducSkuListCount();
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
        public JsonResult AddProductSku([FromBody]ProductSkuDto productSku)
        {
            try
            {
                IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
                var result = service.AddProductSku(productSku);
                JsonResult.Add("result", result);
            }
            catch(Exception ex)
            {
                Common.Common.WriteLog("AddProductSku ex="+ex.ToString());
            }

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DeleteProductSku(string skuId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DeleteProductSku(skuId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult SearchProductByKeyWord(string keywords)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.SearchProductByKeyWord(keywords,  1, 30);
            JsonResult.Add("items", result);
            
            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult AddOneProductSku(string skuId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.AddOneProductSku(skuId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
        public JsonResult DelOneProductSku(string skuId)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var result = service.DelOneProductSku(skuId);
            JsonResult.Add("result", result);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

        public JsonResult SearchProducSku(int schoolId,int districtId,int shopId,string productId,int skuStatus)
        {
            IProductAppService service = IocManager.Instance.Resolve<IProductAppService>();
            var list = service.SearchProducSku(schoolId,  districtId, shopId, productId, skuStatus);
            JsonResult.Add("items", list);

            return new JsonResult()
            {
                Data = JsonResult,
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }

    }
}
