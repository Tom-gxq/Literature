using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Util;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using StockGRPCInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ProductReportDatabase : IReportDatabase
    {
        private readonly ProductRepository _repository;
        private readonly ProductImageRepository _imageRepository;
        private readonly BrandRepository _brandRepository;
        private readonly ProductTypeRepository _typeRepository;
        private readonly ProductAttributeRepository _attributeRepository;
        private readonly SuppliersProductRepository _suppliersProductRepository;
        public ProductReportDatabase(ProductRepository repository, ProductImageRepository imageRepository, BrandRepository brandRepository, 
            ProductTypeRepository typeRepository, ProductAttributeRepository attributeRepository, SuppliersProductRepository suppliersProductRepository)
        {
            _repository = repository;
            _imageRepository = imageRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
            _attributeRepository = attributeRepository;
            _suppliersProductRepository = suppliersProductRepository;
        }

        public ProductEntity GetProductById(string prductId)
        {
            var productEntity = _repository.GetProductById(prductId);
            return productEntity;
        }
        private ProductDomain GetProductDomainById(ProductEntity productEntity)
        {
            var product = new ProductDomain();
            product.SetMemento(productEntity);

            var imageList = _imageRepository.GetProductImageByProductId(productEntity.ProductId);
            product.SetMemenImagesto(imageList);

            if (productEntity.BrandId != null)
            {
                var brand = _brandRepository.GetBrandById(productEntity.BrandId.Value);
                product.SetMemenBrandto(brand);
            }

            if (productEntity.TypeId != null)
            {
                var type = _typeRepository.GetProductTypeById(productEntity.TypeId.Value);
                product.SetMemenTypeto(type);
            }

            var attribute = _attributeRepository.GetAttributeByProductId(productEntity.ProductId);
            product.SetMemenAttributeto(attribute);

            return product;
        }
        public ProductDomain GetProductDomainById(string prductId)
        {
            var product = new ProductDomain();
            var productEntity = _repository.GetProductById(prductId);
            product.SetMemento(productEntity);

            if (productEntity != null)
            {
                if (!string.IsNullOrEmpty(productEntity.ProductId))
                {
                    var imageList = _imageRepository.GetProductImageByProductId(productEntity.ProductId);
                    product.SetMemenImagesto(imageList);
                    var attribute = _attributeRepository.GetAttributeByProductId(productEntity.ProductId);
                    product.SetMemenAttributeto(attribute);
                }

                if (productEntity.BrandId != null)
                {
                    var brand = _brandRepository.GetBrandById(productEntity.BrandId.Value);
                    product.SetMemenBrandto(brand);
                }

                if (productEntity.TypeId != null)
                {
                    var type = _typeRepository.GetProductTypeById(productEntity.TypeId.Value);
                    product.SetMemenTypeto(type);
                }                
            }

            return product;
        }
        public ProductDomain GetSellerProduct(string prductId,string accountId)
        {            
            var productEntity = _repository.GetSellerProduct(prductId,accountId);
            
            if (productEntity != null)
            {
                var product = new ProductDomain();
                product.SetMemento(productEntity);
                if (productEntity.BrandId != null)
                {
                    var brand = _brandRepository.GetBrandById(productEntity.BrandId.Value);
                    product.SetMemenBrandto(brand);
                }
                return product;
            }
            else
            {
                return null;
            }
            
        }

        public List<ProductDomain> GetProductList(int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetProductList(pageIndex, pageSize);
            foreach(var item in productList)
            {
                var domain = GetProductDomainById(item.ProductId);
                domainList.Add(domain);
            }
            return domainList;
        }

        public ProductDomain GetProductDetail(string productId)
        {
            return GetProductDomainById(productId);
        }

        public List<ProductDomain> GetProductListByBrandId(int brandId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetProductListByBrandId(brandId,pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item);
                domainList.Add(domain);
            }
            return domainList;
        }
        public List<CarouselDomain> GetCarouselList()
        {
            var domainList = new List<CarouselDomain>();
            var productList = _repository.GetCarouselList();
            foreach (var item in productList)
            {
                var domain = new CarouselDomain();
                domain.DisplayIndex = item.DisplaySequence;
                domain.ImagePath = item.ImagePath;
                domain.Url = item.Url;
                domainList.Add(domain);
            }
            return domainList;
        }

        public List<ProductDomain> GetProductListByTypeId(long typeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetProductListByTypeId(typeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item);
                domainList.Add(domain);
            }
            return domainList;
        }
        public List<ProductDomain> GetProductListByAttributeId(long attributeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetProductListByAttributeId(attributeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item.ProductId);
                domainList.Add(domain);
            }
            return domainList;
        }
        public List<ProductDomain>  SearchProductKeywordList(string keyword, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.SearchProductKeywordList(keyword, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item);
                domainList.Add(domain);
            }
            return domainList;
        }

        public List<ProductDomain> GetShopProductList(int districtId, int shopId, long typeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetShopProductList(districtId,shopId, typeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item);
                var host = OrderCommon.GetHost();
                var response = StockBusiness.GetProductSku(host, item.ProductId, domain.ShopId);
                if (response.Sku != null && response.Sku.Count > 0)
                {
                    domain.SkuNum = response.Sku[0].Stock;
                    domain.SkuId = response.Sku[0].SkuId;
                }
                domainList.Add(domain);
            }
            return domainList;
        }
        public List<ProductDomain> GetFoodShopProductList(int districtId, int shopId,  int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetFoodShopProductList(districtId, shopId,  pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = GetProductDomainById(item);
                var host = OrderCommon.GetHost();
                try
                {
                    var response = StockBusiness.GetProductSku(host, item.ProductId, shopId);
                    if (response.Sku.Count > 0)
                    {
                        domain.SkuNum = response.Sku[0].Stock;
                        domain.SkuId = response.Sku[0].SkuId;
                        domain.ShopId = response.Sku[0].ShopId;
                    }
                }
                catch(Exception ex)
                {
                    System.Console.WriteLine("GetFoodShopProductList GetProductSku ex="+ex.ToString());
                }
                domainList.Add(domain);
            }
            return domainList;
        }
        public int GetShopProductCount(int districtId, int shopId, long typeId, int pageIndex, int pageSize)
        {
            return  _repository.GetShopProductCount(districtId,shopId, typeId, pageIndex, pageSize);
        }
        public int GetFoodShopProductListCount(int districtId, int shopId,  int pageIndex, int pageSize)
        {
            return _repository.GetFoodShopProductListCount(districtId, shopId, pageIndex, pageSize);
        }

        public bool Add(ProductEntity item)
        {
            return _repository.Add(item);
        }
        
        public bool Update(ProductEntity item)
        {
            var ret= _repository.Update(item);
            return ret > 0;
        }
        public bool Del(string prductId)
        {
            var ret = _repository.Del(prductId);
            return ret > 0;
        }
        public bool AddSuppliersProduct(SuppliersProductEntity item)
        {
                return _suppliersProductRepository.AddSuppliers(item);
        }
        public bool UpdateSuppliersProduct(SuppliersProductEntity item)
        {
            var ret = _suppliersProductRepository.UpdateSuppliers(item);
            return ret > 0;
        }
        public List<ProductDomain> GetDistributorMarketProduct(long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetDistributorMarketProduct(typeId, secondTypeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = new ProductDomain();
                domain.SetMemento(item);
                domainList.Add(domain);
            }
            return domainList;
        }
        public int GetDistributorMarketProductCount(long typeId, long secondTypeId)
        {
            var domainList = new List<ProductDomain>();
            var count = _repository.GetDistributorMarketProductCount(typeId, secondTypeId);

            return count;
        }
        public List<ProductDomain> GetDistributorProduct(int districtId, long secondTypeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetDistributorProduct(districtId, secondTypeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = new ProductDomain();
                domain.SetMemento(item);
                domainList.Add(domain);
            }
            return domainList;
        }
        public int GetDistributorProductCount(long districtId, long secondTypeId)
        {
            var domainList = new List<ProductDomain>();
            var count = _repository.GetDistributorProductCount(districtId, secondTypeId);
            
            return count;
        }

        public List<ProductDomain> GetSellerProduct(string accountId, long typeId, long secondTypeId, int pageIndex, int pageSize)
        {
            var domainList = new List<ProductDomain>();
            var productList = _repository.GetSellerProduct(accountId,typeId, secondTypeId, pageIndex, pageSize);
            foreach (var item in productList)
            {
                var domain = new ProductDomain();
                domain.SetMemento(item);
                domainList.Add(domain);
            }
            return domainList;
        }
        public int GetSellerProductCount(string accountId, long typeId, long secondTypeId)
        {
            var domainList = new List<ProductDomain>();
            var count = _repository.GetSellerProductCount(accountId,typeId, secondTypeId);

            return count;
        }
    }
}
