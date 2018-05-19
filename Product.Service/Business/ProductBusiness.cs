using SP.Service;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Service.Business
{
    public class ProductBusiness
    {
        public static ProductListResponse GetProductList(int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetProductList(pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var product = ConvertProductDomainToResponse(item);
                        result.ProductList.Add(product);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ProdctDetailResponse GetProductDetail(string productId)
        {
            var result = new ProdctDetailResponse();
            result.Status = 10002;
            var detail = ServiceLocator.ReportDatabase.GetProductDetail(productId);
            if (detail != null)
            {
                result.ProductDetail = ConvertProductDomainToResponse(detail);
                result.Status = 10001;
            }
            return result;
        }

        public static ProductListResponse GetProductListByBrandId(int brandId,int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetProductListByBrandId(brandId,pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var product = ConvertProductDomainToResponse(item);
                        result.ProductList.Add(product);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ProductListResponse GetProductListByTypeId(long typeId, int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetProductListByTypeId(typeId, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var product = ConvertProductDomainToResponse(item);
                        result.ProductList.Add(product);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ProductListResponse GetProductListByAttributeId(long attributeId, int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetProductListByAttributeId(attributeId, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var product = ConvertProductDomainToResponse(item);
                        result.ProductList.Add(product);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ProductListResponse SearchProductKeywordList(string keyword, int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;            
            var list = ServiceLocator.ReportDatabase.SearchProductKeywordList(keyword, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var product = ConvertProductDomainToResponse(item);
                        result.ProductList.Add(product);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        public static TitleAttributeListResponse GetTitleAttributeList(int attType)
        {
            var result = new TitleAttributeListResponse();
            result.Status = 10002;
            var list = ServiceLocator.AttributeReportDatabase.GetTitleAttributeList(attType);
            if (list != null)
            {
                var day = (int)DateTime.Now.DayOfWeek;
                
                for (int i =0;i<1;i++)
                {
                    if(day >= list.Count)
                    {
                        day = 0;
                    }
                    var item = list[day];
                    if (item != null)
                    {
                        var attribute = new SP.Service.Attribute();
                        attribute.AttributeId = item.Id;
                        attribute.AttributeName = item.AttributeName;
                        result.TitleAttributeList.Add(attribute);
                    }
                    day++;
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ShopListResponse GetAllShopList(int regionId,int shopType, int pageIndex, int pageSize)
        {
            var result = new ShopListResponse();
            result.Status = 10002;
            result.Total = ServiceLocator.ShopReportDatabase.GetAllShopCount(regionId, shopType);
            if (result.Total > 0)
            {
                var list = ServiceLocator.ShopReportDatabase.GetAllShopList(regionId, shopType, pageIndex, pageSize);
                if (list != null)
                {
                    foreach (var item in list)
                    {
                        if (item != null)
                        {
                            var shop = new SP.Service.Shop();
                            shop.ShopId = item.Id;
                            shop.ShopName = item.ShopName;
                            shop.ShopLogo = string.IsNullOrEmpty(item.ShopLogo) ? string.Empty : item.ShopLogo;
                            if (!string.IsNullOrEmpty(item.OwnerId))
                            {
                                shop.OwnerId = item.OwnerId;
                                var account = ServiceLocator.AccuntInfoReportDatabase.GetAccountInfoById(item.OwnerId);
                                shop.OwnerName = account?.Fullname??string.Empty;
                            }
                            if (!string.IsNullOrEmpty(item.StartTime))
                            {
                                shop.StartTime = item.StartTime;
                            }
                            else
                            {
                                shop.StartTime = string.Empty;
                            }
                            if (!string.IsNullOrEmpty(item.EndTime))
                            {
                                shop.EndTime =  item.EndTime;
                            }
                            else
                            {
                                shop.EndTime = string.Empty;
                            }
                            result.ShopList.Add(shop);
                        }
                    }
                    result.Status = 10001;
                }
            }
            return result;
        }
        public static ProductListResponse GetShopProductList(int districtId, int shopId, long typeId, int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            result.Total = ServiceLocator.ReportDatabase.GetShopProductCount(districtId,shopId, typeId, pageIndex, pageSize);
            if (result.Total > 0)
            {
                var list = ServiceLocator.ReportDatabase.GetShopProductList(districtId,shopId, typeId, pageIndex, pageSize);
                if (list != null)
                {
                    bool skuFlag = false;
                    var shop = ServiceLocator.ShopReportDatabase.GetShopById(shopId);
                    if(!string.IsNullOrEmpty(shop.StartTime))
                    {
                        var startShopTime = DateTime.Parse(DateTime.Now.ToShortDateString()+" "+ shop.StartTime);
                        if(DateTime.Now >= startShopTime)
                        {
                            skuFlag = true;
                        }
                    }
                    foreach (var item in list)
                    {
                        if (item != null)
                        {
                            var product = ConvertProductDomainToResponse(item);
                            if(!skuFlag)
                            {
                                //product.SkuNum = -1;
                            }
                            result.ProductList.Add(product);
                        }
                    }
                    result.Status = 10001;
                }
            }
            return result;
        }
        public static ProductListResponse GetFoodShopProductList(int districtId, int shopId,  int pageIndex, int pageSize)
        {
            var result = new ProductListResponse();
            result.Status = 10002;
            result.Total = ServiceLocator.ReportDatabase.GetFoodShopProductListCount(districtId, shopId, pageIndex, pageSize);
            if (result.Total > 0)
            {
                var list = ServiceLocator.ReportDatabase.GetFoodShopProductList(districtId, shopId, pageIndex, pageSize);
                if (list != null)
                {
                    bool skuFlag = false;
                    var shop = ServiceLocator.ShopReportDatabase.GetShopById(shopId);
                    if (!string.IsNullOrEmpty(shop.StartTime))
                    {
                        var startShopTime = DateTime.Parse(DateTime.Now.ToShortDateString() + " " + shop.StartTime);
                        if (DateTime.Now >= startShopTime)
                        {
                            skuFlag = true;
                        }
                    }
                    foreach (var item in list)
                    {
                        if (item != null)
                        {
                            var product = ConvertProductDomainToResponse(item);
                            if (!skuFlag)
                            {
                                //product.SkuNum = -1;
                            }
                            result.ProductList.Add(product);
                        }
                    }
                    result.Status = 10001;
                }
            }
            return result;
        }

        public static ShopResponse GetShopById(int shopId)
        {
            var result = new ShopResponse();
            result.Status = 10002;
            var shopDomain = ServiceLocator.ShopReportDatabase.GetShopById(shopId);

            if (shopDomain != null)
            {
                result.Shop = new Shop();
                result.Shop.ShopId = shopDomain.Id;
                result.Shop.ShopName = shopDomain.ShopName;
                result.Shop.OwnerId = !string.IsNullOrEmpty(shopDomain.OwnerId)? shopDomain.OwnerId:string.Empty;
                result.Shop.OwnerName = !string.IsNullOrEmpty(shopDomain.OwnerName) ? shopDomain.OwnerName: string.Empty;
                result.Status = 10001;
            }
            return result;
        }

        public static CarouselListResponse GetCarouselList()
        {
            var result = new CarouselListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetCarouselList();
            if (list != null)
            {
                foreach (var item in list)
                {
                    if (item != null)
                    {
                        var carousel = new SP.Service.Carousel();
                        carousel.DisplayIndex = item.DisplayIndex;
                        carousel.ImagePath = !string.IsNullOrEmpty(item.ImagePath) ? item.ImagePath: string.Empty;
                        carousel.Url = !string.IsNullOrEmpty(item.Url)? item.Url:string.Empty;
                        result.CarouselList.Add(carousel);
                    }
                }
                result.Status = 10001;
            }
            return result;
        }

        private static SP.Service.Product ConvertProductDomainToResponse(ProductDomain entity)
        {
            var product = new SP.Service.Product();
            product.ProductId = entity.ProductId;
            product.ProductName = !string.IsNullOrEmpty(entity.ProductName) ? entity.ProductName : string.Empty;
            product.ProductCode = !string.IsNullOrEmpty(entity.ProductCode) ? entity.ProductCode : string.Empty;
            product.MarketPrice = entity.MarketPrice;
            product.Description = !string.IsNullOrEmpty(entity.Description) ? entity.Description : string.Empty;
            product.SaleStatus = entity.SaleStatus;
            product.ShortDescription = !string.IsNullOrEmpty(entity.ShortDescription) ? entity.ShortDescription : string.Empty;
            product.Unit = entity.Unit;
            product.SkuNum = entity.SkuNum;
            product.AddedDate = entity.AddedDate.Ticks;
            product.VipPrice = entity.VipPrice;
            if (entity.Brand != null)
            {
                product.Brand = new Brand();
                product.Brand.BrandId = entity.Brand.Id != null ? entity.Brand.Id.Value : 0;
                product.Brand.BrandName = !string.IsNullOrEmpty(entity.Brand.BrandName) ? entity.Brand?.BrandName : string.Empty;
                product.Brand.Description = !string.IsNullOrEmpty(entity.Brand?.Description??null)? entity.Brand?.Description:string.Empty;
                product.Brand.Logo = !string.IsNullOrEmpty(entity.Brand.Logo) ? entity.Brand?.Logo : string.Empty;
                product.Brand.CompanyUrl = !string.IsNullOrEmpty(entity.Brand.CompanyUrl) ? entity.Brand?.CompanyUrl : string.Empty;
            }
            if(entity.ProductType != null)
            {
                product.ProductType = new ProductType();
                product.ProductType.TypeId = entity.ProductType.Id != null ? entity.ProductType.Id.Value:0;
                product.ProductType.TypeName = !string.IsNullOrEmpty(entity.ProductType.TypeName) ? entity.ProductType.TypeName : string.Empty;
                product.ProductType.Remark = !string.IsNullOrEmpty(entity.ProductType.Remark) ? entity.ProductType.Remark : string.Empty;
                product.ProductType.Kind = entity.ProductType.Kind != null ? entity.ProductType.Kind.Value : 0;
            }
            if(entity.Images != null)
            {
                foreach (var item in entity.Images)
                {
                    var image = new ProductImage();
                    image.Id = item.Id != null ? item.Id.Value:0;
                    image.ImgPath = !string.IsNullOrEmpty(item.ImgPath) ? item.ImgPath : string.Empty;
                    image.Postion = item.Postion!= null?item.Postion.Value:0;
                    product.Image.Add(image);
                }
            }
            return product;
        }

        public static TitleTypeListResponse GetTitleTypeList(int typeKind)
        {
            var result = new TitleTypeListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ProductTypeReportDatabase.GetProductTypeByKind(typeKind);
            if (list != null)
            {
                //var day = (int)DateTime.Now.DayOfWeek;

                //for (int i = 0; i < 1; i++)
                //{
                //    if (day >= list.Count)
                //    {
                //        day = 0;
                //    }
                //    var item = list[day];
                //    if (item != null)
                //    {
                //        var type = new SP.Service.ProductType();
                //        type.TypeId = item.Id;
                //        type.TypeName = item.TypeName;
                //        type.TypePath = string.IsNullOrEmpty(item.TypePath)?string.Empty: item.TypePath;
                //        type.Kind = item.Kind;
                //        type.TypeLogo = string.IsNullOrEmpty(item.TypeLogo) ? string.Empty : item.TypeLogo;
                //        result.TitleTypeList.Add(type);
                //    }
                //    day++;
                //}
                foreach(var item in list)
                {
                    var type = new SP.Service.ProductType();
                    type.TypeId = item.Id;
                    type.TypeName = item.TypeName;
                    type.TypePath = string.IsNullOrEmpty(item.TypePath) ? string.Empty : item.TypePath;
                    type.Kind = item.Kind;
                    type.TypeLogo = string.IsNullOrEmpty(item.TypeLogo) ? string.Empty : item.TypeLogo;
                    result.TitleTypeList.Add(type);
                }
                result.Status = 10001;
            }
            return result;
        }
    }
}
