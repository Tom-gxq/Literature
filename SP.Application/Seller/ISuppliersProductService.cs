using Lib.Application.Services;
using SP.Application.Seller.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Seller
{
    public interface ISuppliersProductService : IApplicationService
    {
        bool AddProduct(SuppliersProductDto data);
        bool DelProduct(int id);
        bool UpdateProduct(SuppliersProductDto dto);
        List<SuppliersProductDto> GetSuppliersProductList(int pageIndex, int pageSize, int sellerId);
        int GetSuppliersProductCount(int sellerId);
        SuppliersProductDto GetSellerProductById(int id);
    }
}
