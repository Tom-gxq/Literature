using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Timer.Service
{
    public class ServiceLocator
    {
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();
        private static ICommandBus _commandBus;
        private static ProductSkuReportDatabase _reportDatabase;
        private static AccountProductReportDatabase _accountProductReportDatabase;
        private static SellerProductReportDatabase _sellerProductReportDatabase;
        private static SuppliersReportDatabase _suppliersReportDatabase;
        private static ShipOrderReportDatabase _shipOrderReportDatabase;
        private static ShopReportDatabase _shopReportDatabase;
        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(ProductSkuReportDatabase)) as ProductSkuReportDatabase;
                    _sellerProductReportDatabase = IocManager.Instance.Resolve(typeof(SellerProductReportDatabase)) as SellerProductReportDatabase;
                    _suppliersReportDatabase = IocManager.Instance.Resolve(typeof(SuppliersReportDatabase)) as SuppliersReportDatabase;
                    _shipOrderReportDatabase = IocManager.Instance.Resolve(typeof(ShipOrderReportDatabase)) as ShipOrderReportDatabase;
                    _shopReportDatabase = IocManager.Instance.Resolve(typeof(ShopReportDatabase)) as ShopReportDatabase;
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static ProductSkuReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }
        public static AccountProductReportDatabase AccountProductReportDatabase
        {
            get { return _accountProductReportDatabase; }
        }
        public static SellerProductReportDatabase SellerProductReportDatabase
        {
            get { return _sellerProductReportDatabase; }
        }
        public static SuppliersReportDatabase SuppliersReportDatabase
        {
            get { return _suppliersReportDatabase; }
        }
        public static ShipOrderReportDatabase ShipOrderReportDatabase
        {
            get { return _shipOrderReportDatabase; }
        }
        public static ShopReportDatabase ShopReportDatabase
        {
            get { return _shopReportDatabase; }
        }
    }
}
