using Lib.Application.Services;
using LibMain.Dependency;
using LibMain.Domain.Repositories;
using SP.Application.Product.DTO;
using SP.Application.User.DTO;
using SP.DataEntity;
using SP.ManageEntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Product
{
    public class ProductAppService : ApplicationService, IProductAppService
    {
        private readonly IRepository<ProductEntity> _productRepository;
        private readonly IRepository<AdminEntity, long> _userRepository;
        private readonly IRepository<ProductImageEntity> _imageRepository;
        private readonly IRepository<ProductAttributeEntity> _attributeRepository;
        private readonly IRepository<BrandEntity> _brandRepository;
        private readonly IRepository<ProductTypeEntity> _typeRepository;

        public ProductAppService(IRepository<ProductEntity> productRepository, IRepository<AdminEntity, long> userRepository, 
            IRepository<ProductImageEntity> imageRepository, IRepository<ProductAttributeEntity> attributeRepository,
            IRepository<BrandEntity> brandRepository, IRepository<ProductTypeEntity> typeRepository)
        {
            _productRepository = productRepository;
            _userRepository = userRepository;
            _imageRepository = imageRepository;
            _attributeRepository = attributeRepository;
            _brandRepository = brandRepository;
            _typeRepository = typeRepository;
        }

        public bool AddProduct(ProductsDto product)
        {
           var lastOperater =(AdminDto)this.AbpSession["current"];
           var reuslt= _productRepository.Insert(new ProductEntity()
            {
                ProductId = Guid.NewGuid().ToString(),
                ProductName = product.ProductName,
                Description = product.Description,
                AddedDate = DateTime.Now,
                ProductCode = product.ProductCode,
                Unit = product.Unit,
                MarketPrice = product.MarketPrice,
                SaleStatus=0,
                Meta_Keywords = $"{product.ProductName}|{product.ProductCode}",
                ShortDescription = product.ShortDescription,
                DisplaySequence = product.DisplaySequence,
                UpdateTime = DateTime.Now,
                LastOperater = lastOperater!= null ? lastOperater.Id:0,
                VIPPrice = product.VIPPrice
           });
            return reuslt >0;
        }

        public List<ProductsDto> GetProductList(int saleStatus, int pageIndex, int pageSize)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.GetProductList(saleStatus, pageIndex, pageSize);
            foreach (var item in list)
            {
                var userRepository = IocManager.Instance.Resolve<AdminRespository>();
                var operater = item?.LastOperater != null ? userRepository.GetAdminById(item.LastOperater.Value)?.UserName : string.Empty;
                var product = ConvertFromRepositoryEntity(item, operater);

                if (item.BrandId != null && item.BrandId.Value > 0)
                {
                    var brand = _brandRepository.Single(x => x.Id == item.BrandId.Value);
                    product.Brand = ConvertBrandFromRepositoryEntity(brand);
                }
                if (item.TypeId != null && item.TypeId.Value > 0)
                {
                    var productType = _typeRepository.Single(x=>x.Id == item.TypeId.Value);
                    product.ProductType = ConvertTypeFromRepositoryEntity(productType);
                }

                retList.Add(product);
            }
            return retList;
        }
        public List<ProductsDto> GetShopProductList(int shopId, int saleStatus, int pageIndex, int pageSize)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.GetShopProductList(shopId,saleStatus, pageIndex, pageSize);
            foreach (var item in list)
            {
                var userRepository = IocManager.Instance.Resolve<AdminRespository>();
                var operater = item?.LastOperater != null ? userRepository.GetAdminById(item.LastOperater.Value)?.UserName : string.Empty;
                var product = ConvertFromRepositoryEntity(item, operater);

                if (item.BrandId != null && item.BrandId.Value > 0)
                {
                    var brand = _brandRepository.Single(x => x.Id == item.BrandId.Value);
                    product.Brand = ConvertBrandFromRepositoryEntity(brand);
                }
                if (item.TypeId != null && item.TypeId.Value > 0)
                {
                    var productType = _typeRepository.Single(x => x.Id == item.TypeId.Value);
                    product.ProductType = ConvertTypeFromRepositoryEntity(productType);
                }

                retList.Add(product);
            }
            return retList;
        }

        public int GetProductListCount(int saleStatus)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var total = repository.GetProductListCount(saleStatus);
            return total;
        }
        public int GetShopProductListCount(int shopId, int saleStatus)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var total = repository.GetShopProductListCount(shopId,saleStatus);
            return total;
        }

        public bool AddProductBrand(string productId, int brandId)
        {
            var result = _productRepository.UpdateNonDefaults(new ProductEntity()
            {
                ProductId = productId,
                BrandId = brandId
            },x=>x.ProductId == productId);
            return result > 0;
        }

        public bool AddProductType(string productId, int typeId)
        {
            var result = _productRepository.UpdateNonDefaults(new ProductEntity()
            {
                ProductId = productId,
                TypeId = typeId
            }, x => x.ProductId == productId);
            return result > 0;
        }

        public bool AddProductImage(ProductImageDto image)
        {
            var result = _imageRepository.Insert(new ProductImageEntity()
            {
                ImgPath = image.ImgPath,
                Postion = image.Postion != null ? image.Postion:0,
                ProductId = image.ProductId,
                IsDel = false,
                CreateTime = DateTime.Now,
                UpdateTime = DateTime.Now,                
            });
            return result >0;
        }

        public bool UpdateProductImageDisplaySequence(int id, int sequence)
        {
            var reuslt = _imageRepository.UpdateNonDefaults(new ProductImageEntity()
            {
               DisplaySequence = sequence,
            },x=>x.Id == id);
            return reuslt > 0;
        }

        public bool DeleteProductImageById(int id)
        {
            var result = false;
            try
            {
                _imageRepository.Delete(id);
                result = true;
            }
            catch
            {

            }
            return result;
        }

        public bool AddProductAttribute(ProductAttributeDto attribute)
        {
            var productAttr = _attributeRepository.Single(x => x.ProductId == attribute.ProductId && x.AttributeId == attribute.AttributeId);
            if (productAttr != null)
            {
                return true;
            }
            else
            {
                var result = _attributeRepository.Insert(new ProductAttributeEntity()
                {
                    AttributeId = attribute.AttributeId,
                    ProductId = attribute.ProductId,
                });
                return result > 0;
            }
        }

        public bool DeleteProductAttribute(string productId, long attributeId)
        {
            var result = false;
            try
            {
                _attributeRepository.Delete(x => x.ProductId == productId && x.AttributeId == attributeId);
                result = true;
            }
            catch
            {

            }
            return result;
        }
        public bool DeleteProductImage(string productId, long imageId)
        {
            var result = false;
            try
            {
                _imageRepository.Delete(x => x.ProductId == productId && x.Id == imageId);
                result = true;
            }
            catch
            {

            }
            return result;
        }

        public bool SetProductAttributeValue(string productId, long attributeId,long valueId)
        {
            var result = _attributeRepository.UpdateNonDefaults(new ProductAttributeEntity()
            {
               ProductId = productId,
               AttributeId = attributeId,
               ValueId = valueId
            },x=>x.ProductId == productId && x.AttributeId == attributeId);
            return result > 0;
        }

        public List<ProductsDto> SearchProductList(string keyWord, int typeId, int brandId, int saleStatus, int pageIndex, int pageSize)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.SearchProductList(keyWord, typeId, brandId, saleStatus, pageIndex, pageSize);
            foreach (var item in list)
            {
                var userRepository = IocManager.Instance.Resolve<AdminRespository>();
                var operater = item?.LastOperater != null ? userRepository.GetAdminById(item.LastOperater.Value)?.UserName : string.Empty;
                var order = ConvertFromRepositoryEntity(item, operater);
                retList.Add(order);
            }
            return retList;
        }
        public int SearchProductListCount(string keyWord, int typeId, int brandId, int saleStatus)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var total = repository.SearchProductListCount(keyWord, typeId, brandId, saleStatus);
            return total;
        }

        public ProductsDto GetProductDetail(string productId)
        {
            var result = _productRepository.Single(x => x.ProductId == productId);

            var product = ConvertFromRepositoryEntity(result);
            if(product != null)
            {
                var imageList = _imageRepository.GetAllList(x=>x.ProductId == productId).OrderBy(x=>x.DisplaySequence).ToList();
                product.ProductImage = new List<ProductImageDto>();
                foreach (var item in imageList)
                {
                    var image = ConvertImageFromRepositoryEntity(item);
                    if (image != null)
                    {
                        product.ProductImage.Add(image);
                    }
                }
                var repository = IocManager.Instance.Resolve<ProductsRespository>();
                var brand = repository.GetProductBrandByProductId(productId);
                product.Brand = ConvertBrandFromRepositoryEntity(brand);

                var productType = repository.GetProductProductTypeByProductId(productId);
                product.ProductType = ConvertTypeFromRepositoryEntity(productType);

            }
            return product;
        }

        public bool UpdateProductSaleStatus(string productId, int saleStatus)
        {
            var result = _productRepository.UpdateNonDefaults(new ProductEntity()
            {
                ProductId = productId,
                SaleStatus = saleStatus
            }, x => x.ProductId == productId);
            return result > 0;
        }

        public bool DeleteProduct(string productId)
        {
            var result = _productRepository.Delete(x => x.ProductId == productId);
            return result > 0;
        }

        public bool EditProduct(ProductsDto product)
        {
            var lastOperater = (AdminDto)this.AbpSession["current"];
            int? typeId = null;
            if(product.TypeId > 0)
            {
                typeId = product.TypeId;
            }
            int? brandId = null;
            if (product.BrandId > 0)
            {
                brandId = product.BrandId;
            }
            var result = _productRepository.UpdateNonDefaults(new ProductEntity()
            {
                ProductName = product.ProductName,
                Description = product.Description,
                ProductCode = product.ProductCode,
                Unit = product.Unit,
                MarketPrice = product.MarketPrice,
                Meta_Keywords = $"{product.ProductName}|{product.ProductCode}",
                ShortDescription = product.ShortDescription,
                DisplaySequence = product.DisplaySequence,
                UpdateTime = DateTime.Now,
                TypeId = typeId,
                BrandId = brandId,
                LastOperater = lastOperater != null ? lastOperater.Id : 0,
                VIPPrice = product.VIPPrice,
            },x=>x.ProductId == product.ProductId);
            return result > 0;
        }

        public List<AttributeDto> GetAttributeList(string productId)
        {
            var retList = new List<AttributeDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.GetProductAttributeByProductId(productId);
            foreach (var item in list)
            {
                var attribute = ConvertAttributeFromRepositoryEntity(item);                               

                retList.Add(attribute);
            }
            return retList;
        }
        public List<ProductImageDto> GetImageList(string productId)
        {
            var retList = new List<ProductImageDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.GetImageListByProductId(productId);
            foreach (var item in list)
            {
                var image = ConvertImageFromRepositoryEntity(item);

                retList.Add(image);
            }
            return retList;
        }

        public List<ProductsDto> GetProductListByOrderId(string orderId)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.GetProductListByOrderId(orderId);
            foreach (var item in list)
            {
                var product = GetProductDetail(item.ProductId);
                product.Quantity = item.Quantity;

                retList.Add(product);
            }
            return retList;
        }
        public List<ProductSkuDto> GetProducSkuList(int pageIndex, int pageSize)
        {
            var retList = new List<ProductSkuDto>();
            var repository = IocManager.Instance.Resolve<ProductSkuRespository>();
            var list = repository.GetProductSkuList(pageIndex, pageSize);
            foreach(var item in list)
            {
                var entity = ConvertSkuFromRepositoryEntity(item);
                retList.Add(entity);
            }
            return retList;
        }
        public long GetProducSkuListCount()
        {
            var retList = new List<ProductSkuDto>();
            var repository = IocManager.Instance.Resolve<ProductSkuRespository>();
            var count = repository.GetProductSkuListCount();
            return count;
        }
        public bool AddProductSku(ProductSkuDto productSku)
        {
            var repository = IocManager.Instance.Resolve<ProductSkuRespository>();
            return repository.AddProductSku(new ProductSkuEntity()
            {
               AlertStock = productSku.AlertStock,
               EffectiveTime = DateTime.Now,
               Id = Guid.NewGuid().ToString(),
               Price = productSku.Price,
               ProductId = productSku.ProductId,
               SKU = productSku.SKU,
               Stock = productSku.Stock
            });
        }

        public bool DeleteProductSku(string skuId)
        {
            var repository = IocManager.Instance.Resolve<ProductSkuRespository>();
            return repository.DeleteProductSku(skuId);
        }
        public List<ProductsDto> SearchProductByKeyWord(string keyWord, int pageIndex, int pageSize)
        {
            var retList = new List<ProductsDto>();
            var repository = IocManager.Instance.Resolve<ProductsRespository>();
            var list = repository.SearchProductByKeyWord(keyWord, pageIndex, pageSize);
            foreach (var item in list)
            {
                var order = ConvertFromRepositoryEntity(item, string.Empty);
                retList.Add(order);
            }
            return retList;
        }

        private static ProductsDto ConvertFromRepositoryEntity(ProductEntity product,string lastOperater="")
        {
            if (product == null)
            {
                return null;
            }
            var productsDto = new ProductsDto
            {
                MarketPrice = product.MarketPrice!= null ?product.MarketPrice.Value:0,
                VIPPrice = product.VIPPrice != null ? product.VIPPrice.Value : 0,
                AddedDate = product.AddedDate != null ? product.AddedDate.Value:DateTime.MinValue,
                Description = product.Description,
                ProductCode = product.ProductCode,
                LastOperater = lastOperater,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription,
                Unit = product.Unit,       
                DisplaySequence = product.DisplaySequence != null ? product.DisplaySequence.Value:0,
                SaleStatus = product.SaleStatus!= null ? product.SaleStatus.Value:0
            };

            return productsDto;
        }
        private static ProductsDto ConvertFromRepositoryEntity(ProductFullEntity product, string lastOperater = "")
        {
            if (product == null)
            {
                return null;
            }
            var productsDto = new ProductsDto
            {
                MarketPrice = product.MarketPrice != null ? product.MarketPrice.Value : 0,
                VIPPrice = product.VIPPrice != null ? product.VIPPrice.Value : 0,
                AddedDate = product.AddedDate != null ? product.AddedDate.Value : DateTime.MinValue,
                Description = product.Description,
                ProductCode = product.ProductCode,
                LastOperater = lastOperater,
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                ShortDescription = product.ShortDescription,
                Unit = product.Unit,
                ShopId = product.ShopId,
                Quantity = product.Quantity,
                DisplaySequence = product.DisplaySequence != null ? product.DisplaySequence.Value : 0,
                SaleStatus = product.SaleStatus != null ? product.SaleStatus.Value : 0
            };

            return productsDto;
        }

        private static ProductImageDto ConvertImageFromRepositoryEntity(ProductImageEntity productImge)
        {
            if (productImge == null)
            {
                return null;
            }
            var productImageDto = new ProductImageDto
            {
                ProductId = productImge.ProductId,
                Id = productImge.Id,
                ImgPath = productImge.ImgPath,
                Postion = productImge.Postion,                
            };

            return productImageDto;
        }

        private static BrandDto ConvertBrandFromRepositoryEntity(BrandEntity brand)
        {
            if (brand == null)
            {
                return null;
            }
            var pbrandDto = new BrandDto
            {
                 BrandId = brand.Id,
                 BrandName = brand.BrandName,
                 Logo = brand.Logo,
                 CompanyUrl = brand.CompanyUrl,
                 Description = brand.Description,
                 MetaDescription = brand.MetaDescription
            };

            return pbrandDto;
        }

        private static ProductTypeDto ConvertTypeFromRepositoryEntity(ProductTypeEntity productType)
        {
            if (productType == null)
            {
                return null;
            }
            var productTypeDto = new ProductTypeDto
            {
                 TypeId = productType.Id,
                 TypeName = productType.TypeName,
                 Remark = productType.Remark,
                 Kind = productType.Kind != null? productType.Kind.Value:0,
            };

            return productTypeDto;
        }
        private static AttributeDto ConvertAttributeFromRepositoryEntity(AttributeEntity attribute)
        {
            if (attribute == null)
            {
                return null;
            }
            var attributeDto = new AttributeDto
            {
                Id = attribute.Id,
                AttributeName = attribute.AttributeName,
                DisplaySequence = attribute.DisplaySequence != null ? attribute.DisplaySequence.Value:0
            };

            return attributeDto;
        }

        private static ProductSkuDto ConvertSkuFromRepositoryEntity(ProductSkuFullEntity productSku)
        {
            if (productSku == null)
            {
                return null;
            }
            var skuDto = new ProductSkuDto
            {
                SKU = productSku.SKU,
                AlertStock = productSku.AlertStock.Value,
                EffectiveTime = productSku.EffectiveTime.Value,
                Price = productSku.Price.Value,
                ProductId = productSku.ProductId,
                ProductName = productSku.ProductName,
                SkuId = productSku.Id,
                Stock = productSku.Stock.Value
            };

            return skuDto;
        }

    }
}
