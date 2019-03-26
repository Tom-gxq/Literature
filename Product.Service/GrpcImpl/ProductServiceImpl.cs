using Grpc.Core;
using Microsoft.Extensions.Logging;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SP.Service.ProductService;
using Microsoft.Extensions.DependencyInjection;
using Product.Service.Business;

namespace Product.Service.GrpcImpl
{
    public partial class ProductServiceImpl: ProductServiceBase
    {
        private ILogger logger = new ServiceCollection()
             .AddLogging()
             .BuildServiceProvider()
             .GetService<ILoggerFactory>()
             .AddConsole()
             .CreateLogger("ProductService");

        private int prjLicEID = 7000;

        public ProductServiceImpl(int port)
        {
            if (port > 0)
            {
                this.prjLicEID = port;
            }
        }

        public override Task<ProductListResponse> GetProductList(ProductListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetProductList(request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProdctDetailResponse> GetProductDetail(ProductIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductDetail {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ProdctDetailResponse response = null;
            try
            {
                response = ProductBusiness.GetProductDetail(request.ProductId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductDetail Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductDetail {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> GetProductListByBrandId(BrandIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductListByBrandId {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetProductListByBrandId(request.BrandId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductListByBrandId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductListByBrandId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> GetProductListByTypeId(TypeIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductListByTypeId {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetProductListByTypeId(request.TypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductListByTypeId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductListByTypeId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> GetProductListByAttributeId(AttributeIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductListByAttributeId {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetProductListByAttributeId(request.AttributeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductListByAttributeId Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductListByAttributeId {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> SearchProductKeywordList(SearchProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "SearchProductKeywordList {Date} {IPAdress} {Status} Connected! KeyWord:[{KeyWord}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),request.KeyWord);
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.SearchProductKeywordList(request.KeyWord, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "SearchProductKeywordList Exception");
            }
            logger.LogInformation(this.prjLicEID, "SearchProductKeywordList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<TitleAttributeListResponse> GetTitleAttributeList(TitleAttributeListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetTitleAttributeList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            TitleAttributeListResponse response = null;
            try
            {
                response = ProductBusiness.GetTitleAttributeList(request.AttType);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetTitleAttributeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetTitleAttributeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShopListResponse> GetAllShopList(ShopListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetAllShopList {Date} {IPAdress} {Status} Connected! DistrictId:[{DistrictId}] ShopType:[{ShopType}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), 
                context.Peer, context.Status.ToString(), request.DistrictId, request.ShopType);
            ShopListResponse response = null;
            try
            {
                response = ProductBusiness.GetAllShopList(request.DistrictId, request.ShopType, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAllShopList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAllShopList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductListResponse> GetShopProductList(ShopProductListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetShopProductList {Date} {IPAdress} {Status} Connected! DistrictId:[{DistrictId}] ShopId:[{ShopId}] TypeId:[{TypeId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DistrictId, request.ShopId, request.TypeId);
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetShopProductList(request.DistrictId,request.ShopId, request.TypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShopProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShopProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ProductListResponse> GetFoodShopProductList(ShopProductListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetFoodShopProductList {Date} {IPAdress} {Status} Connected! DistrictId:[{DistrictId}]  ShopId:[{ShopId}] ]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.DistrictId,request.ShopId);
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetFoodShopProductList(request.DistrictId, request.ShopId,  request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetFoodShopProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetFoodShopProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ShopResponse> GetShopById(ShopIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetShopById {Date} {IPAdress} {Status} Connected! ShopId:[{ShopId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.ShopId);
            ShopResponse response = null;
            try
            {
                response = ProductBusiness.GetShopById(request.ShopId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShopById Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShopById {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<CarouselListResponse> GetCarouselList(VoidRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetCarouselList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            CarouselListResponse response = null;
            try
            {
                response = ProductBusiness.GetCarouselList();
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetCarouselList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetCarouselList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<TitleTypeListResponse> GetShopTypeList(VoidRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetShopTypeList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            TitleTypeListResponse response = null;
            try
            {
                response = ProductBusiness.GetTitleTypeList(0);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShopTypeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShopTypeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<TitleTypeListResponse> GetProductTypeList(VoidRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductTypeList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            TitleTypeListResponse response = null;
            try
            {
                response = ProductBusiness.GetTitleTypeList(1);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductTypeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductTypeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ResultResponse> UpdateOpenShopStatus(OpenShopStatusRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateOpenShopStatus Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {Status} ", request.AccountId, request.Status);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.UpdateOpenShopStatus(request.AccountId, request.Status);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateOpenShopStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateOpenShopStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }        

        public override Task<ResultResponse> DelProduct(ProductIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} DelProduct Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {ProductId}  ",request.ProductId );
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.DelProduct(request.ProductId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "DelProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "DelProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ResultResponse> UpdateProductSaleStatus(ProductSaleStatusRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateProductSaleStatus Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {ProductId}  {Status} ", request.ProductId, request.Status);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.UpdateProductSaleStatus(request.ProductId, request.Status);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateProductSaleStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateProductSaleStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductDetailResponse> GetSellerProductDetail(ProductIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSellerProductDetail {Date} {IPAdress} {Status} Connected! {ProductId}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.ProductId);
            ProductDetailResponse response = null;
            try
            {
                response = ProductBusiness.GetSellerProductDetail(request.ProductId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSellerProductDetail Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSellerProductDetail {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SellerProductListResponse> GetDistributorMarketProduct(ShopProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetDistributorMarketProduct {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{TypeId} {SecondTypeId} {PageIndex} {PageSize}", request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            SellerProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetDistributorMarketProduct(request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetDistributorMarketProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetDistributorMarketProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SellerProductListResponse> GetDistributorFoodShopProductList(SellerShopProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetDistributorFoodShopProductList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {TypeId} {SecondTypeId} {PageIndex} {PageSize}", request.AccountId,request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            SellerProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetDistributorProduct(request.AccountId,request.SecondTypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetDistributorFoodShopProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetDistributorFoodShopProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<SellerProductListResponse> GetSellerMarketProduct(SellerShopProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSellerMarketProduct {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {TypeId} {SecondTypeId} {PageIndex} {PageSize}", request.AccountId,request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            SellerProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetSellerProduct(request.AccountId,request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSellerMarketProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSellerMarketProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<SellerProductListResponse> GetSellerFoodShopProductList(SellerShopProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSellerFoodShopProductList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {TypeId} {SecondTypeId} {PageIndex} {PageSize}", request.AccountId, request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            SellerProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetSellerProduct(request.AccountId, request.TypeId, request.SecondTypeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSellerFoodShopProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSellerFoodShopProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductTypeListResponse> GetAllProductTypeList(KindRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetAllProductTypeList {Date} {IPAdress} {Status} Connected!  Kind={Kind} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(),request.Kind);
            ProductTypeListResponse response = null;
            try
            {
                response = ProductBusiness.GetAllProductTypeList(request.Kind);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAllProductTypeList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAllProductTypeList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ShopStatusResponse> GetShopStatus(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetShopStatus {Date} {IPAdress} {Status} Connected!  AccountId={AccountId} ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.AccountId);
            ShopStatusResponse response = null;
            try
            {
                response = ProductBusiness.GetShopStatus(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShopStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShopStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ResultResponse> AddProduct(ProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddProduct Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {MainType} {SecondType} {ProductId}  {PurchasePrice} {SuppliersId} ",
                request.AccountId, request.MainType, request.SecondType, request.ProductId, request.PurchasePrice, request.SuppliersId);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.AddProduct(request.AccountId, request.MainType, request.SecondType, request.ProductId, request.PurchasePrice, request.SuppliersId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ResultResponse> UpdateProduct(ProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateProduct Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId}  {ProductId}  {PurchasePrice} {SuppliersId} ",
                request.AccountId, request.ProductId, request.PurchasePrice, request.SuppliersId);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.UpdateProduct(request.ProductId, request.PurchasePrice, request.SuppliersId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ResultResponse> AddSuppliersRegion(AddSuppliersRegionRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddSuppliersRegion Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {SupplierId} {RegionId} ",request.SupplierId, request.RegionId);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.AddSuppliersRegion(request.SupplierId, request.RegionId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddSuppliersRegion Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddSuppliersRegion {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

       
    }
}
