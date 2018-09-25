using Microsoft.Extensions.DependencyInjection;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using static SP.Service.StockService;
using System.Threading.Tasks;
using SP.Service;
using Stock.Service.Business;

namespace Stock.Service.GrpcImpl
{
    public class StockServiceImpl: StockServiceBase
    {
        private ILogger logger = new ServiceCollection()
              .AddLogging()
              .BuildServiceProvider()
              .GetService<ILoggerFactory>()
              .AddConsole()
              .CreateLogger("StockService");

        private int prjLicEID = 7000;

        public StockServiceImpl(int port)
        {
            if (port > 0)
            {
                this.prjLicEID = port;
            }
        }

        public override Task<ProductSkuResponse> GetProductSku(ProductSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {ProductId} {ShopId} ", request.ProductId, request.ShopId);
            ProductSkuResponse response = null;
            try
            {
                response = ProductSkuBusiness.GetProductSku(request.ProductId, request.ShopId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ProductSkuResponse> GetAccountProductSku(AccountProductSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetAccountProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {ProductId} {ShopId} {AccountId}", request.ProductId, request.ShopId, request.AccountId);
            ProductSkuResponse response = null;
            try
            {
                response = ProductSkuBusiness.GetAccountProductSku(request.ProductId, request.ShopId, request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetAccountProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetAccountProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }


        public override Task<SkuStatusResponse> DecreaseProductSku(OperationSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "DecreaseProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {ProductId} {ShopId} {Stock}", request.AccountId,request.ProductId, request.ShopId, request.Stock);
            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.DecreaseProductSku(request.OrderId,request.AccountId,request.ProductId, request.ShopId, request.Stock);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "DecreaseProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "DecreaseProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SkuStatusResponse> RedoProductSku(OperationSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "RedoProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {ProductId} {ShopId} {Stock}", request.AccountId, request.ProductId, request.ShopId, request.Stock);
            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.RedoProductSku(request.AccountId, request.ProductId, request.ShopId, request.Stock);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "RedoProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "RedoProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SkuStatusResponse> AddInvProductSku(InvSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "AddInvProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{ProductId} {ShopId}", request.ProductId, request.ShopId);
            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.AddInvProductSku(request.ProductId, request.ShopId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddInvProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddInvProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SkuStatusResponse> DelInvProductSku(InvSkuRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "DelInvProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{ProductId} {ShopId}", request.ProductId, request.ShopId);
            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.DelInvProductSku(request.ProductId, request.ShopId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "DelInvProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "DelInvProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SkuStatusResponse> UpdateProductSku(SkuListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateProductSku {Date} {IPAdress} {Status} Connected!  TimeSpan={TimeSpan}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.TimeSpan);
            
            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.UpdateProductSku(request.Sku, request.TimeSpan);               
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SkuStatusResponse> DelProductSku(SkuListRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "DelProductSku {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());

            SkuStatusResponse response = null;
            try
            {
                response = ProductSkuBusiness.DelProductSku(request.Sku);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "DelProductSku Exception");
            }
            logger.LogInformation(this.prjLicEID, "DelProductSku {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
