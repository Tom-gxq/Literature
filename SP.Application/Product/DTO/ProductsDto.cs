using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product.DTO
{
    public class ProductsDto
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ShortDescription { get; set; }
        public string Unit { get; set; }
        public string Description { get; set; }
        public int SaleStatus { get; set; }
        public int ShopId { get; set; }
        public DateTime AddedDate { get; set; }
        public int DisplaySequence { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal VIPPrice { get; set; }
        public DateTime UpdateTime { get; set; }
        public string LastOperater { get; set; }
        public int TypeId { get; set; }
        public int SecondTypeId { get; set; }
        public int BrandId { get; set; }
        public BrandDto Brand { get; set; }
        public ProductTypeDto ProductType { get; set; }
        public List<ProductImageDto> ProductImage { get; set; }
        public List<AttributeDto> ProductAttribute { get; set; }
        public int Quantity { get; set; }
        public double PurchasePrice { get; set; }
    }
}
