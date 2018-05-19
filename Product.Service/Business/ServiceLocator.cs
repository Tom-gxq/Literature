using Grpc.Service.Core.Dependency;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Product.Service.Business
{
    public class ServiceLocator
    {
        private static ProductReportDatabase _reportDatabase;
        private static AttributeReportDatabase _attributeReportDatabase;
        private static ShopReportDatabase _shopReportDatabase;
        private static AccountInfoReportDatabase _accuntInfoReportDatabase;
        private static ProductTypeReportDatabase _productTypeReportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _reportDatabase = IocManager.Instance.Resolve(typeof(ProductReportDatabase)) as ProductReportDatabase;
                    _attributeReportDatabase = IocManager.Instance.Resolve(typeof(AttributeReportDatabase)) as AttributeReportDatabase;
                    _shopReportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                    _accuntInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                    _productTypeReportDatabase = IocManager.Instance.Resolve(typeof(ProductTypeReportDatabase)) as ProductTypeReportDatabase;
                    _isInitialized = true;
                }
            }
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
    }
}
