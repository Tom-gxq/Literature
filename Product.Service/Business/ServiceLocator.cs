using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Service.Business
{
    public class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static ProductReportDatabase _reportDatabase;
        private static AttributeReportDatabase _attributeReportDatabase;
        private static ShopReportDatabase _shopReportDatabase;
        private static AccountInfoReportDatabase _accuntInfoReportDatabase;
        private static ProductTypeReportDatabase _productTypeReportDatabase;
        private static ProductImageReportDatabase _productImageReportDatabase;
        private static AddressReportDatabase _addressReportDatabase;
        private static SuppliersReportDatabase _suppliersReportDatabase;
        private static SellerProductReportDatabase _sellerProductReportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase;
                    _attributeReportDatabase = IocManager.Instance.Resolve(typeof(AttributeReportDatabase)) as AttributeReportDatabase;
                    _shopReportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                    _accuntInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                    _productTypeReportDatabase = IocManager.Instance.Resolve(typeof(ProductTypeReportDatabase)) as ProductTypeReportDatabase;
                    _productImageReportDatabase = IocManager.Instance.Resolve(typeof(ProductImageReportDatabase)) as ProductImageReportDatabase;
                    _addressReportDatabase = IocManager.Instance.Resolve(typeof(AddressReportDatabase)) as AddressReportDatabase;
                    _suppliersReportDatabase = IocManager.Instance.Resolve(typeof(SuppliersReportDatabase)) as SuppliersReportDatabase;
                    _sellerProductReportDatabase = IocManager.Instance.Resolve(typeof(SellerProductReportDatabase)) as SellerProductReportDatabase;
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static ProductReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }

        public static AttributeReportDatabase AttributeReportDatabase
        {
            get { return _attributeReportDatabase; }
        }

        public static ShopReportDatabase ShopReportDatabase
        {
            get { return _shopReportDatabase; }
        }

        public static AccountInfoReportDatabase AccuntInfoReportDatabase
        {
            get { return _accuntInfoReportDatabase; }
        }
        public static ProductTypeReportDatabase ProductTypeReportDatabase
        {
            get { return _productTypeReportDatabase; }
        }
        public static ProductImageReportDatabase ProductImageReportDatabase
        {
            get { return _productImageReportDatabase; }
        }
        public static AddressReportDatabase AddressDatabase
        {
            get { return _addressReportDatabase; }
        }

        public static SuppliersReportDatabase SuppliersReportDatabase
        {
            get { return _suppliersReportDatabase; }
        }
        public static SellerProductReportDatabase SellerProductReportDatabase
        {
            get { return _sellerProductReportDatabase; }
        }
    }
}
