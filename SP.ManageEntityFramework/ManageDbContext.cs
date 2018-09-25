using Lib.EntityFramework.EntityFramework.Interface;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework
{
    public class ManageDbContext : Lib.EntityFramework.EntityFramework.LibDbContext
    {
        public IDbSet<AdminEntity> Admin { get; set; }
        public IDbSet<ProductEntity> Product { get; set; }
        public IDbSet<ProductImageEntity> ProductImage { get; set; }
        public IDbSet<ProductAttributeEntity> ProductAttribute { get; set; }
        public IDbSet<AccountEntity> Account { get; set; }
        public IDbSet<AccountInfoEntity> AccountInfo { get; set; }
        public IDbSet<AttributeEntity> Attribute { get; set; }
        public IDbSet<AttributeValueEntity> AttributeValue { get; set; }
        public IDbSet<BrandEntity> Brand { get; set; }
        public IDbSet<OrdersEntity> Order { get; set; }
        public IDbSet<ProductRegionEntity> ProductRegion { get; set; }
        public IDbSet<ProductTypeBrandEntity> ProductTypeBrand { get; set; }
        public IDbSet<ProductTypeEntity> ProductType { get; set; }
        public IDbSet<ShopEntity> Shop { get; set; }
        public IDbSet<ShopAttributeEntity> ShopAttribute { get; set; }
        public IDbSet<RegionEntity> Region { get; set; }
        public IDbSet<ShopProductEntity> ShopProduct { get; set; }
        public IDbSet<SysKindEntity> SysKind { get; set; }
        public IDbSet<CarouselEntity> Carousel { get; set; }
        public IDbSet<CashApplyEntity> CashApply { get; set; }
        public IDbSet<SuppliersEntity> Suppliers { get; set; }
        public IDbSet<ShippingOrdersEntity> ShippingOrder { get; set; }
        public IDbSet<SellerStatisticsEntity> SellerStatistics { get; set; }
        public IDbSet<AssociatorEntity> Associator { get; set; }

        public ManageDbContext()
            : base("Default")
        {

        }
        public ManageDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }
    }
}
