using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Api.Model.Product
{
    public class ProductModel
    {
        /**
        * 商品ID
        */
        public string productId { get; set; }
        /**
        * 商品名
        */
        public string productName { get; set; }
        /**
        * 
        */
        public string productCode { get; set; }
        /**
        * 
        */
        public string shortDescription { get; set; }
        /**
        * 
        */
        public string unit { get; set; }
        /**
        * 
        */
        public string description { get; set; }
        /**
        * 
        */
        public int saleStatus { get; set; }
        /**
        * 
        */
        public int skuNum { get; set; }
        /**
        * 
        */
        public DateTime addedDate { get; set; }
        /**
        * 
        */
        public double marketPrice { get; set; }
        /**
        * 
        */
        public double vipPrice { get; set; }
        /**
        * 
        */
        public BrandModel brand { get; set; }
        /**
        * 
        */
        public string suppliersId { get; set; }
        /**
        * 
        */
        public ProductTypeModel productType { get; set; }
        /**
        * 
        */
        public ProductAttributeModel productAttribute { get; set; }
        /**
        * 
        */
        public List<ProductImageModel> images { get; set; }
}
}
