﻿//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace AgentDashboard.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class SPEntities : DbContext
    {
        public SPEntities()
            : base("name=SPEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<AccountAuthentication> AccountAuthentication { get; set; }
        public virtual DbSet<OAuth2_Access_Token> OAuth2_Access_Token { get; set; }
        public virtual DbSet<SP_Account> SP_Account { get; set; }
        public virtual DbSet<SP_AccountAddress> SP_AccountAddress { get; set; }
        public virtual DbSet<SP_AccountFinance> SP_AccountFinance { get; set; }
        public virtual DbSet<SP_AccountInfo> SP_AccountInfo { get; set; }
        public virtual DbSet<SP_AccountProduct> SP_AccountProduct { get; set; }
        public virtual DbSet<SP_Admin> SP_Admin { get; set; }
        public virtual DbSet<SP_ApplyPartner> SP_ApplyPartner { get; set; }
        public virtual DbSet<SP_Associator> SP_Associator { get; set; }
        public virtual DbSet<SP_Attribute> SP_Attribute { get; set; }
        public virtual DbSet<SP_AttributeValue> SP_AttributeValue { get; set; }
        public virtual DbSet<SP_BrandCategories> SP_BrandCategories { get; set; }
        public virtual DbSet<SP_Carousel> SP_Carousel { get; set; }
        public virtual DbSet<SP_CashApply> SP_CashApply { get; set; }
        public virtual DbSet<SP_ConsumeTrade> SP_ConsumeTrade { get; set; }
        public virtual DbSet<SP_EventRelation> SP_EventRelation { get; set; }
        public virtual DbSet<SP_IncomeTrade> SP_IncomeTrade { get; set; }
        public virtual DbSet<SP_Orders> SP_Orders { get; set; }
        public virtual DbSet<SP_ProductAttribute> SP_ProductAttribute { get; set; }
        public virtual DbSet<SP_ProductImage> SP_ProductImage { get; set; }
        public virtual DbSet<SP_ProductRegion> SP_ProductRegion { get; set; }
        public virtual DbSet<SP_Products> SP_Products { get; set; }
        public virtual DbSet<SP_ProductSKUs> SP_ProductSKUs { get; set; }
        public virtual DbSet<SP_ProductType> SP_ProductType { get; set; }
        public virtual DbSet<SP_ProductTypeBrand> SP_ProductTypeBrand { get; set; }
        public virtual DbSet<SP_RegionAccount> SP_RegionAccount { get; set; }
        public virtual DbSet<SP_RegionData> SP_RegionData { get; set; }
        public virtual DbSet<SP_RegionType> SP_RegionType { get; set; }
        public virtual DbSet<SP_ResEvent> SP_ResEvent { get; set; }
        public virtual DbSet<SP_SellerStatistics> SP_SellerStatistics { get; set; }
        public virtual DbSet<SP_SellerStatisticsTrade> SP_SellerStatisticsTrade { get; set; }
        public virtual DbSet<SP_ShipOrder> SP_ShipOrder { get; set; }
        public virtual DbSet<SP_ShippingOrders> SP_ShippingOrders { get; set; }
        public virtual DbSet<SP_ShipStatistics> SP_ShipStatistics { get; set; }
        public virtual DbSet<SP_Shop> SP_Shop { get; set; }
        public virtual DbSet<SP_ShopAttribute> SP_ShopAttribute { get; set; }
        public virtual DbSet<SP_ShoppingCarts> SP_ShoppingCarts { get; set; }
        public virtual DbSet<SP_ShopProduct> SP_ShopProduct { get; set; }
        public virtual DbSet<SP_Suppliers> SP_Suppliers { get; set; }
        public virtual DbSet<SP_SuppliersProduct> SP_SuppliersProduct { get; set; }
        public virtual DbSet<SP_SuppliersRegion> SP_SuppliersRegion { get; set; }
        public virtual DbSet<SP_SysEvent> SP_SysEvent { get; set; }
        public virtual DbSet<SP_SysStatistics> SP_SysStatistics { get; set; }
        public virtual DbSet<SP_Trade> SP_Trade { get; set; }
        public virtual DbSet<SP_UserShipping> SP_UserShipping { get; set; }
        public virtual DbSet<SP_RepeatedToken> SP_RepeatedToken { get; set; }
        public virtual DbSet<SP_ShopOwner> SP_ShopOwner { get; set; }
        public virtual DbSet<SP_SysKind> SP_SysKind { get; set; }
    }
}
