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
    public class ProductServiceImpl: ProductServiceBase
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
            logger.LogInformation(this.prjLicEID, "GetAllShopList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            ShopListResponse response = null;
            try
            {
                response = ProductBusiness.GetAllShopList(request.DistrictId, request.PageIndex, request.PageSize);
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
            logger.LogInformation(this.prjLicEID, "GetShopProductList {Date} {IPAdress} {Status} Connected! ShopId:[{ShopId}]", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.ShopId);
            ProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetShopProductList(request.DistrictId,request.ShopId, request.AttributeId, request.PageIndex, request.PageSize);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetShopProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetShopProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
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
    }
}
