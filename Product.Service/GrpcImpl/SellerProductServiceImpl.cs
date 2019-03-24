using Grpc.Core;
using Microsoft.Extensions.Logging;
using Product.Service.Business;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SP.Service.ProductService;

namespace Product.Service.GrpcImpl
{
    public partial class ProductServiceImpl : ProductServiceBase
    {
        public override Task<ResultResponse> SelectSellerProduct(SelectSellerProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "SelectSellerProduct {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {SupplierProductId} {IsSelected}  ", request.AccountId, request.SupplierProductId, request.IsSelected);
            ResultResponse response = null;
            try
            {
                if(request.IsSelected)
                {
                    response = ProductBusiness.AddSellerProduct(request.AccountId, request.SupplierProductId);
                }
                else
                {
                    response = ProductBusiness.DelSellerProduct(request.AccountId, request.SupplierProductId);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "SelectSellerProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "SelectSellerProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ResultResponse> UpdateSupplierProductSaleStatus(SupplierProductSaleStatusRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateSupplierProductSaleStatus {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {ProductId} {SuppliersId} {SaleStatus}  ", request.ProductId, request.SuppliersId, request.SaleStatus);
            ResultResponse response = null;
            try
            {
                response = ProductBusiness.UpdateSupplierProductSaleStatus(request.ProductId, request.SuppliersId, request.SaleStatus);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateSupplierProductSaleStatus Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateSupplierProductSaleStatus {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<ResultResponse> AddSuppliersProduct(AddSuppliersProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} AddSuppliersProduct Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId} {MainType} {SecondType} {ProductId}  {PurchasePrice} {SuppliersId} ",
                request.AccountId, request.MainType, request.SecondType, request.ProductId, request.PurchasePrice, request.SuppliersId);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.AddSuppliersProduct(request.AccountId, request.MainType, request.SecondType, request.ProductId, request.PurchasePrice, request.SuppliersId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddSuppliersProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddSuppliersProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<ResultResponse> UpdateSuppliersProduct(ProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "{Date} {IPAdress} {Status} UpdateSuppliersProduct Connected!",
                DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, " {AccountId}  {ProductId}  {PurchasePrice} {SuppliersId} ",
                request.AccountId, request.ProductId, request.PurchasePrice, request.SuppliersId);
            var response = new ResultResponse();
            response.Status = 10002;
            try
            {
                response = ProductBusiness.UpdateSuppliersProduct(request.ProductId, request.PurchasePrice, request.SuppliersId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "UpdateSuppliersProduct Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateSuppliersProduct {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SuppliersTypeResponse> GetSuppliersType(SupplierIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSuppliersType {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{SupplierId}", request.SupplierId);
            SuppliersTypeResponse response = null;
            try
            {
                response = ProductBusiness.GetSuppliersType(request.SupplierId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSuppliersType Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSuppliersType {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<SupplierInfoResponse> GetSupplierInfo(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSupplierInfo {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId}", request.AccountId);
            SupplierInfoResponse response = null;
            try
            {
                response = ProductBusiness.GetSupplierInfo(request.AccountId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSupplierInfo Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSupplierInfo {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SuppliersProductListResponse> GetSuppliersProducts(SuppliersProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSuppliersProducts {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{MainType} {SecondType} {SupplierId}", request.MainType, request.SecondType, request.SupplierId);
            SuppliersProductListResponse response = null;
            try
            {
                response = ProductBusiness.GetSuppliersProducts(request.MainType, request.SecondType, request.SupplierId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSuppliersProducts Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSuppliersProducts {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SellerFoodProductListResponse> GetSellerFoodProductList(SellerFoodProductRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSellerFoodProductList {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "{AccountId} {RegionId} {PageIndex} {PageSize} {IsSelected}", request.AccountId,request.RegionId, request.PageIndex, request.PageSize, request.IsSelected);
            SellerFoodProductListResponse response = null;
            try
            {
                if(request.IsSelected)
                {
                    response = ProductBusiness.GetSellerFoodProductList(request.RegionId, request.AccountId, request.PageIndex, request.PageSize);
                }
                else
                {
                    response = ProductBusiness.GetSellerProductListByAccountId(request.AccountId, request.PageIndex, request.PageSize);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSellerFoodProductList Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSellerFoodProductList {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<SuppliersProductResponse> GetSuppliersProductById(SupplierIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetSuppliersProductById {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "SupplierId={SupplierId}", request.SupplierId);
            SuppliersProductResponse response = null;
            try
            {
                response = ProductBusiness.GetSuppliersProductById(request.SupplierId);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetSuppliersProductById Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetSuppliersProductById {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
