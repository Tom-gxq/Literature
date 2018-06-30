using Lib.Application.Services;
using SP.Application.Product.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public interface IProductAppService : IApplicationService
    {
        bool AddProduct(ProductsDto product);
        List<ProductsDto> GetProductList(int saleStatus,int pageIndex, int pageSize);
        List<ProductsDto> GetShopProductList(int shopId, int saleStatus, int pageIndex, int pageSize);
        bool AddProductBrand(string productId,int brandId);
        bool AddProductType(string productId, int typeId);
        bool AddProductImage(ProductImageDto image);
        bool UpdateProductImageDisplaySequence(int id ,int sequence);
        bool DeleteProductImageById(int id);
        bool AddProductAttribute(ProductAttributeDto attribute);
        bool DeleteProductAttribute(string productId,long attributeId);
        bool DeleteProductImage(string productId, long imageId);
        bool SetProductAttributeValue(string productId, long attributeId, long valueId);
        List<ProductsDto> SearchProductList(string keyWord, int typeId, int brandId, int saleStatus, int pageIndex, int pageSize);
        ProductsDto GetProductDetail(string productId);
        bool UpdateProductSaleStatus(string productId, int saleStatus);
        bool DeleteProduct(string productId);
        bool EditProduct(ProductsDto product);
        int GetProductListCount(int saleStatus);
        int GetShopProductListCount(int shopId, int saleStatus);
        int SearchProductListCount(string keyWord, int typeId, int brandId, int saleStatus);
        List<AttributeDto> GetAttributeList(string productId);
        List<ProductImageDto> GetImageList(string productId);
        List<ProductsDto> GetProductListByOrderId(string orderId);
        List<ProductSkuDto> GetProducSkuList(int pageIndex, int pageSize);
        long GetProducSkuListCount();
        bool AddProductSku(ProductSkuDto productSku);
        bool DeleteProductSku(string skuId);
        List<ProductsDto> SearchProductByKeyWord(string keyWord, int pageIndex, int pageSize);
        bool AddOneProductSku(string skuId);
        bool DelOneProductSku(string skuId);
        List<ProductSkuDto> SearchProducSku(int schoolId, int districtId, int shopId, string productId, int skuStatus);
        List<ProductSkuDto> GetMarketSkuList(int pageIndex, int pageSize, int marketId);
        long GetMarketSkuListCount(int marketId);
    }
}
