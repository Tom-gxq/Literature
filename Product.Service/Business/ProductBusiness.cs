using Grpc.Service.Core.Dependency;
using Microsoft.Extensions.Configuration;
using SP.Service;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Linq;
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
                    var plist = list.GroupBy(x=>x.ProductId);
                    foreach (var item in plist)
                    {
                        if (item != null)
                        {
                            var product = ConvertProductDomainToResponse(item.FirstOrDefault());
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
                var list = ServiceLocator.ReportDatabase.GetFoodShopProductList(districtId, shopId, pageIndex, (int)result.Total);
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
                    var plist = list.GroupBy(x => x.ProductId);
                    var sortList = new List<ProductDomain>();
                    foreach(var item in plist)
                    {
                        sortList.Add(item.FirstOrDefault());
                    }
                    sortList = sortList.OrderByDescending(x => x.SkuNum).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                    foreach (var item in sortList)
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
            product.Unit = !string.IsNullOrEmpty(entity.Unit) ? entity.Unit : string.Empty; ;
            product.SkuNum = entity.SkuNum;
            product.AddedDate = entity.AddedDate.Ticks;
            product.VipPrice = entity.VipPrice;
            product.ShopId = entity.ShopId;
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

        public static ResultResponse UpdateOpenShopStatus(string accountId, bool status)
        {
            var result = new ResultResponse();
            result.Status = 10002;
            var ret = ServiceLocator.ShopReportDatabase.UpdateOpenShopStatus(accountId, status);
            if(ret)
            {
                result.Status = 10001;
            }
            else
            {
                result.Status = 10003;
            }
            return result;
        }

        public static ResultResponse AddProduct(string accountId, long mainType, long secondType, string productId, double purchasePrice,int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new CreateProductCommand(Guid.NewGuid(), mainType, secondType, productId, accountId,  purchasePrice, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse UpdateProduct(string productId, double purchasePrice, int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new EditProductCommand(new Guid(productId), productId,  purchasePrice, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse DelProduct(string productId)
        {
            ServiceLocator.CommandBus.Send(new DelProductCommand(new Guid(productId)));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse UpdateProductSaleStatus(string productId, int status)
        {
            //ServiceLocator.CommandBus.Send(new EditSaleStatusCommand(new Guid(productId), status));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ProductDetailResponse GetSellerProductDetail(string productId)
        {
            var result = new ProductDetailResponse();
            result.Status = 10002;
            var product = ServiceLocator.ReportDatabase.GetProductById(productId);
            if (product != null)
            {
                result.Product = new SellerProduct();
                result.Product.MainType = product.TypeId!=null? product.TypeId.Value:0;
                result.Product.SecondType = product.SecondTypeId != null ? product.SecondTypeId.Value : 0;
                result.Product.ProductName = product.ProductName;
                result.Product.MarketPrice = product.MarketPrice != null ? product.MarketPrice.Value : 0;
                result.Product.PurchasePrice = product.PurchasePrice != null ? product.PurchasePrice.Value : 0;
                result.Product.ProductId = product.ProductId;
                result.Product.SuppliersId = product.SuppliersId.ToString();
                var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(productId);
                if (image != null)
                {
                    result.Product.ImagePath = image.ImgPath;
                }
                result.Status = 10001;
            }
            
            return result;
        }
        public static SellerProductListResponse GetDistributorProduct(string accountId,long secondTypeId, int pageIndex, int pageSize)
        {
            var result = new SellerProductListResponse();
            result.Status = 10002;
            var account = ServiceLocator.AddressDatabase.GetDefaultSelectedAddress(accountId);
            if (account != null)
            {
                var list = ServiceLocator.ReportDatabase.GetDistributorProduct(account.DistrictId, secondTypeId, pageIndex, pageSize);
                foreach (var item in list)
                {
                    var product = new SellerProduct();
                    product.ProductName = item.ProductName;
                    product.PurchasePrice = item.PurchasePrice;
                    product.MarketPrice = item.MarketPrice;
                    product.MainType = item.TypeId;
                    product.SecondType = item.SecondTypeId;
                    product.ProductId = item.ProductId;
                    var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                    if (image != null)
                    {
                        product.ImagePath = image.ImgPath;
                    }
                    result.ProductList.Add(product);
                }
                result.Total = ServiceLocator.ReportDatabase.GetDistributorProductCount(account.DistrictId, secondTypeId);
                result.Status = 10001;
            }
            else
            {
                result.Status = 10003;//没有默认地址
            }
            return result;
        }
        public static SellerProductListResponse GetDistributorMarketProduct(long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            var result = new SellerProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetDistributorMarketProduct(typeId, secondTypeId, pageIndex, pageSize);
            foreach (var item in list)
            {
                var product = new SellerProduct();
                product.ProductName = item.ProductName;
                product.PurchasePrice = item.PurchasePrice;
                product.MarketPrice = item.MarketPrice;
                product.MainType = item.TypeId;
                product.SecondType = item.SecondTypeId;
                product.ProductId = item.ProductId;
                var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                if (image != null)
                {
                    product.ImagePath = image.ImgPath;
                }
                result.ProductList.Add(product);
            }
            result.Total = ServiceLocator.ReportDatabase.GetDistributorMarketProductCount(typeId, secondTypeId);
            result.Status = 10001;
            return result;
        }

        public static SellerProductListResponse GetSellerProduct(string accountId,long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            var result = new SellerProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ReportDatabase.GetSellerProduct(accountId, typeId, secondTypeId, pageIndex, pageSize);
            foreach (var item in list)
            {
                var product = new SellerProduct();
                product.ProductName = item.ProductName;
                product.PurchasePrice = item.PurchasePrice;
                product.MarketPrice = item.MarketPrice;
                product.MainType = item.TypeId;
                product.SecondType = item.SecondTypeId;
                product.ProductId = item.ProductId;
                product.SaleStatus = item.SaleStatus; 
                var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                if (image != null)
                {
                    product.ImagePath = image.ImgPath;
                }
                result.ProductList.Add(product);
            }
            result.Total = ServiceLocator.ReportDatabase.GetSellerProductCount(accountId, typeId, secondTypeId);
            result.Status = 10001;
            return result;
        }

        public static ProductTypeListResponse GetAllProductTypeList(int typeKind)
        {
            var result = new ProductTypeListResponse();
            result.Status = 10002;
            var list = ServiceLocator.ProductTypeReportDatabase.GetProductTypeByKind(typeKind);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var type = new SP.Service.ProductType();
                    type.TypeId = item.Id;
                    type.TypeName = item.TypeName;
                    type.TypePath = string.IsNullOrEmpty(item.TypePath) ? string.Empty : item.TypePath;
                    type.Kind = item.Kind;
                    type.TypeLogo = string.IsNullOrEmpty(item.TypeLogo) ? string.Empty : item.TypeLogo;
                    result.ProductTypeList.Add(type);
                }
                result.Status = 10001;
            }
            return result;
        }

        public static ShopStatusResponse GetShopStatus(string accountId)
        {
            var result = new ShopStatusResponse();
            result.Status = 10002;
            var shopOwner = ServiceLocator.ShopReportDatabase.GetShopStatus(accountId);
            if (shopOwner != null)
            {
                result.ShopStatus = shopOwner.ShopStatus;
                result.Status = 10001;
            }
            return result;
        }
        public static ResultResponse AddSuppliersProduct(string accountId, long mainType, long secondType, string productId, double purchasePrice, int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new CreateSuppliersProductCommand(Guid.NewGuid(), accountId, mainType, secondType, productId, purchasePrice, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse AddSuppliersRegion(int supplierId, int regionId)
        {
            ServiceLocator.CommandBus.Send(new CreateSuppliersRegionCommand(supplierId, regionId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static SellerProductListResponse GetSuppliersProduct(int supplierId)
        {
            var result = new SellerProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.SuppliersReportDatabase.GetSuppliersProduct(supplierId);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var product = new SellerProduct();
                    product.ProductName = item.ProductName;
                    product.PurchasePrice = item.PurchasePrice ?? 0;                    
                    product.ProductId = item.ProductId;
                    var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                    if (image != null)
                    {
                        product.ImagePath = image.ImgPath;
                    }
                    result.ProductList.Add(product);
                }
                result.Status = 10001;
            }
            return result;
        }


        public static ResultResponse AddSellerProduct(string accountId, int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new CreateSellerProductCommand(Guid.NewGuid(),  accountId, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse DelSellerProduct(string accountId, int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new DelSellerProductCommand(Guid.NewGuid(), accountId, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }

        public static ResultResponse UpdateSupplierProductSaleStatus(string productId, int suppliersId,int saleStatus)
        {
            ServiceLocator.CommandBus.Send(new EditSaleStatusCommand(Guid.NewGuid(), saleStatus, suppliersId, productId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }
        public static ResultResponse UpdateSuppliersProduct(string productId, double purchasePrice, int suppliersId)
        {
            ServiceLocator.CommandBus.Send(new EditProductCommand(Guid.NewGuid(), productId, purchasePrice, suppliersId));
            var result = new ResultResponse();
            result.Status = 10001;
            return result;
        }
        public static SuppliersTypeResponse GetSuppliersType(int supplierId)
        {
            var result = new SuppliersTypeResponse();
            result.Status = 10002;
            var domain = ServiceLocator.ProductTypeReportDatabase.GetSuppliersType(supplierId);
            if (domain != null)
            {
                result.ProductType = new ProductType();
                result.ProductType.TypeId = domain.Id;
                result.ProductType.TypeName = domain.TypeName;
                result.ProductType.Kind = domain.Kind;
                result.Status = 10001;
            }
            return result;
        }
        public static SupplierInfoResponse GetSupplierInfo(string accountId)
        {
            var result = new SupplierInfoResponse();
            result.Status = 10002;
            var domain = ServiceLocator.SuppliersReportDatabase.GetSupplierInfo(accountId);
            if (domain != null)
            {
                result.SuppliersId = domain.Id.Value;
                result.SuppliersName = domain.SuppliersName;
                result.TypeId = domain.TypeId != null ? domain.TypeId.Value:0;
                result.AccountId = domain.AccountId;
                result.AlipayNo = domain.AlipayNo;
                result.CellPhone = domain.TelPhone;
                result.Status = 10001;
            }
            return result;
        }

        public static SuppliersProductListResponse GetSuppliersProducts(int mainType, int secondType, int supplierId)
        {
            var result = new SuppliersProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.SuppliersReportDatabase.GetSuppliersProduct(mainType, secondType, supplierId);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var product = new SuppliersProduct();
                    product.SuppliersId = item.SuppliersId.Value;
                    product.ProductName = item.ProductName;
                    product.PurchasePrice = item.PurchasePrice ?? 0;
                    product.ProductId = item.ProductId;
                    product.SaleStatus = item.SaleStatus != null ?item.SaleStatus.Value:0;
                    var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                    if (image != null)
                    {
                        product.ImagePath = image.ImgPath;
                    }
                    result.ProductList.Add(product);
                }
                result.Status = 10001;
            }
            return result;
        }

        public static SellerFoodProductListResponse GetSellerFoodProductList(int regionId, string accountId,int pageIndex, int pageSize)
        {
            var result = new SellerFoodProductListResponse();
            result.Status = 10002;
            var config = IocManager.Instance.Resolve<IConfigurationRoot>();
            int typeId = -1;
            if (config != null)
            {
                var reObj = config.GetSection("FoodId");
                int.TryParse(reObj?.Value, out typeId);
            }

            var list = ServiceLocator.SuppliersReportDatabase.GetSellerProductList(regionId, typeId, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var product = new SellerFoodProduct();
                    product.SuppliersId = item.SuppliersId.Value;
                    product.ProductName = item.ProductName;
                    product.PurchasePrice = item.PurchasePrice ?? 0;
                    product.ProductId = item.ProductId;
                    var entity = ServiceLocator.SellerProductReportDatabase.GetSellerProduct(accountId, item.Id.Value);
                    if (entity != null)
                    {
                        product.SelectedStatus = 1;
                    }
                    product.SupplierProductId = item.Id.Value;
                    var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                    if (image != null)
                    {
                        product.ImagePath = image.ImgPath;
                    }
                    result.ProductList.Add(product);
                }
                result.Status = 10001;
            }
            return result;
        }

        public static SellerFoodProductListResponse GetSellerProductListByAccountId(string accountId, int pageIndex, int pageSize)
        {
            var result = new SellerFoodProductListResponse();
            result.Status = 10002;
            var list = ServiceLocator.SuppliersReportDatabase.GetSellerProductListByAccountId(accountId, pageIndex, pageSize);
            if (list != null)
            {
                foreach (var item in list)
                {
                    var product = new SellerFoodProduct();
                    product.SuppliersId = item.SuppliersId.Value;
                    product.ProductName = item.ProductName;
                    product.PurchasePrice = item.PurchasePrice ?? 0;
                    product.ProductId = item.ProductId;
                    product.SupplierProductId = item.Id.Value;
                    var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(item.ProductId);
                    if (image != null)
                    {
                        product.ImagePath = image.ImgPath;
                    }
                    result.ProductList.Add(product);
                }
                result.Status = 10001;
            }
            return result;
        }

        public static SuppliersProductResponse GetSuppliersProductById(int supplierProductId)
        {
            var result = new SuppliersProductResponse();
            result.Status = 10002;
            var domain = ServiceLocator.SuppliersReportDatabase.GetSuppliersProductById(supplierProductId);
            if (domain != null)
            {
                result.Product = new SuppliersProduct()
                {
                   ProductId = domain.ProductId,
                   ProductName = domain.ProductName,
                   PurchasePrice = domain.PurchasePrice.Value,
                   SuppliersId = domain.SuppliersId.Value,
                   SaleStatus = domain.SaleStatus.Value
                };
                var image = ServiceLocator.ProductImageReportDatabase.GetProductImage(domain.ProductId);
                if (image != null)
                {
                    result.Product.ImagePath = image.ImgPath;
                }
                result.Status = 10001;
            }
            return result;
        }
    }
}
