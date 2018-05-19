using SP.Api.Model;
using SP.Api.Model.Product;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProductGRPCInterface
{
    public class ProductBusiness
    {
        public static List<ShopModel> GetAllShopList(int regionId,int shopType,int pageIndex,int pageSize)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new ShopListRequest()
            {
                DistrictId = regionId,
                ShopType = shopType,
                PageIndex = pageIndex,
                PageSize = pageSize
            };
            var result = client.GetAllShopList(request1);
            var list = new List<ShopModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ShopList)
                {
                    var domain = new ShopModel();
                    domain.shopId = item.ShopId;
                    domain.shopName = item.ShopName;
                    domain.ownerId = item.OwnerId;
                    domain.ownerName = item.OwnerName;
                    domain.startTime = item.StartTime;
                    domain.endTime = item.EndTime;
                    domain.shopLogo = item.ShopLogo;
                    list.Add(domain);
                }
            }
            return list;
        }

        public static List<AttributeModel> GetTitleAttributeList()
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new TitleAttributeListRequest()
            {
                AttType = 1
            };
            var result = client.GetTitleAttributeList(request1);
            var list = new List<AttributeModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.TitleAttributeList)
                {
                    var domain = new AttributeModel();
                    domain.attributeId = item.AttributeId;
                    domain.attributeName = item.AttributeName;
                    domain.attributeImage = item.UseAttributeImage;
                    list.Add(domain);
                }
            }
            return list;
        }

        public static List<ProductModel> GetShopProductList(int districtId, long typeId, int pageIndex,int pageSize)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new ShopProductListRequest()
            {
                TypeId = typeId,
                ShopId = 0,
                PageIndex = pageIndex,
                PageSize = pageSize,
                DistrictId = districtId,
            };
            var result = client.GetShopProductList(request1);
            var list = new List<ProductModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ProductList)
                {
                    var domain = new ProductModel();
                    domain.productId = item.ProductId;
                    domain.addedDate = new DateTime(item.AddedDate);
                    domain.description = item.Description;
                    domain.shortDescription = item.ShortDescription;
                    domain.suppliersId = item.SuppliersId;
                    domain.saleStatus = item.SaleStatus;
                    domain.skuNum = item.SkuNum;
                    domain.productName = item.ProductName;
                    domain.marketPrice = item.MarketPrice;
                    domain.vipPrice = item.VipPrice;
                    domain.productCode = item.ProductCode;
                    domain.unit = item.Unit;                    
                    if (item.Brand != null)
                    {
                        domain.brand = new BrandModel()
                        {
                            brandId = item.Brand != null ? item.Brand.BrandId:0,
                            brandName = item.Brand != null ? item.Brand.BrandName : string.Empty,
                            description = item.Brand != null ? item.Brand.Description : string.Empty,
                        };
                    }
                    if(item.ProductType != null)
                    {
                        domain.productType = new ProductTypeModel()
                        {
                           typeId = item.ProductType != null ? item.ProductType.TypeId : 0,
                           typeName = item.ProductType != null ? item.ProductType.TypeName : string.Empty,
                        };
                    }
                    if(item.Image != null)
                    {
                        domain.images = new List<ProductImageModel>();
                        foreach (var imageItem in item.Image)
                        {
                            var imageModel = new ProductImageModel()
                            {
                                id = imageItem != null ? imageItem.Id : 0,
                                imgPath = imageItem != null ? imageItem.ImgPath : string.Empty,
                                postion = imageItem != null ? imageItem.Postion : 0,
                            };
                            domain.images.Add(imageModel);
                        }
                    }
                    list.Add(domain);
                }
            }
            return list;
        }
        public static List<ProductModel> GetFoodShopProductList(int districtId, int shopId, int pageIndex, int pageSize)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new ShopProductListRequest()
            {
                ShopId = shopId,
                PageIndex = pageIndex,
                PageSize = pageSize,
                DistrictId = districtId,
            };
            var result = client.GetFoodShopProductList(request1);
            var list = new List<ProductModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.ProductList)
                {
                    var domain = new ProductModel();
                    domain.productId = item.ProductId;
                    domain.addedDate = new DateTime(item.AddedDate);
                    domain.description = item.Description;
                    domain.shortDescription = item.ShortDescription;
                    domain.suppliersId = item.SuppliersId;
                    domain.saleStatus = item.SaleStatus;
                    domain.skuNum = item.SkuNum;
                    domain.productName = item.ProductName;
                    domain.marketPrice = item.MarketPrice;
                    domain.vipPrice = item.VipPrice;
                    domain.productCode = item.ProductCode;
                    domain.unit = item.Unit;
                    if (item.Brand != null)
                    {
                        domain.brand = new BrandModel()
                        {
                            brandId = item.Brand != null ? item.Brand.BrandId : 0,
                            brandName = item.Brand != null ? item.Brand.BrandName : string.Empty,
                            description = item.Brand != null ? item.Brand.Description : string.Empty,
                        };
                    }
                    if (item.ProductType != null)
                    {
                        domain.productType = new ProductTypeModel()
                        {
                            typeId = item.ProductType != null ? item.ProductType.TypeId : 0,
                            typeName = item.ProductType != null ? item.ProductType.TypeName : string.Empty,
                        };
                    }
                    if (item.Image != null)
                    {
                        domain.images = new List<ProductImageModel>();
                        foreach (var imageItem in item.Image)
                        {
                            var imageModel = new ProductImageModel()
                            {
                                id = imageItem != null ? imageItem.Id : 0,
                                imgPath = imageItem != null ? imageItem.ImgPath : string.Empty,
                                postion = imageItem != null ? imageItem.Postion : 0,
                            };
                            domain.images.Add(imageModel);
                        }
                    }
                    list.Add(domain);
                }
            }
            return list;
        }

        public static ShopModel GetShopById(int shopId)
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new ShopIdRequest()
            {
                ShopId = shopId
            };
            var result = client.GetShopById(request1);
            var domain = new ShopModel();
            if (result.Status == 10001&& result.Shop != null)
            {                
                domain.shopId = result.Shop.ShopId;
                domain.shopName = result.Shop.ShopName;
                domain.ownerId = result.Shop.OwnerId;
                domain.ownerName = result.Shop.OwnerName;
            }
            return domain;
        }
        public static List<CarouselModel> GetCarouselList()
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new VoidRequest();
            var result = client.GetCarouselList(request1);
            var list = new List<CarouselModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.CarouselList)
                {
                    var domain = new CarouselModel();
                    domain.ImagePath = item.ImagePath;
                    domain.Url = item.Url;
                    domain.DisplayIndex = item.DisplayIndex;
                    list.Add(domain);
                }
            }
            return list;
        }
        public static List<ProductTypeModel> GetTitleTypeList()
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new VoidRequest();
            var result = client.GetShopTypeList(request1);
            var list = new List<ProductTypeModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.TitleTypeList)
                {
                    var domain = new ProductTypeModel();
                    domain.typeId = item.TypeId;
                    domain.typeName = item.TypeName;
                    domain.typeLogo = item.TypeLogo;
                    domain.typePath = item.TypePath;
                    list.Add(domain);
                }
            }
            return list;
        }
        public static List<ProductTypeModel> GetProductTypeTitleList()
        {
            var client = ProductClientHelper.GetClient();
            var request1 = new VoidRequest();
            var result = client.GetProductTypeList(request1);
            var list = new List<ProductTypeModel>();
            if (result.Status == 10001)
            {
                foreach (var item in result.TitleTypeList)
                {
                    var domain = new ProductTypeModel();
                    domain.typeId = item.TypeId;
                    domain.typeName = item.TypeName;
                    domain.typeLogo = item.TypeLogo;
                    domain.typePath = item.TypePath;
                    list.Add(domain);
                }
            }
            return list;
        }
    }
}
