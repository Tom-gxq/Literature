using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.Business
{
    public sealed class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static AccountReportDatabase _reportDatabase;
        private static AddressReportDatabase _reportAddressDatabase;
        private static ShoppingCartReportDatabase _reportShppingCartDatabase;
        private static AuthenticationReportDatabase _authenticationReportDatabase;
        private static AssociatorReportDatabase _associatorReportDatabase;
        private static SysKindReportDatabase _kindReportDatabase;
        private static AccountInfoReportDatabase _accountInfoReportDatabase;
        private static TradeReportDatabase _tradeReportDatabase;
        private static CouponsReportDatabase _couponsReportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus; ;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(AccountReportDatabase)) as AccountReportDatabase;
                    _reportAddressDatabase = IocManager.Instance.Resolve(typeof(AddressReportDatabase)) as AddressReportDatabase;
                    _reportShppingCartDatabase = IocManager.Instance.Resolve(typeof(ShoppingCartReportDatabase)) as ShoppingCartReportDatabase;
                    _authenticationReportDatabase = IocManager.Instance.Resolve(typeof(AuthenticationReportDatabase)) as AuthenticationReportDatabase;
                    _associatorReportDatabase = IocManager.Instance.Resolve(typeof(AssociatorReportDatabase)) as AssociatorReportDatabase;
                    _kindReportDatabase = IocManager.Instance.Resolve(typeof(SysKindReportDatabase)) as SysKindReportDatabase;
                    _accountInfoReportDatabase = IocManager.Instance.Resolve(typeof(AccountInfoReportDatabase)) as AccountInfoReportDatabase;
                    _tradeReportDatabase = IocManager.Instance.Resolve(typeof(TradeReportDatabase)) as TradeReportDatabase;
                    _couponsReportDatabase = IocManager.Instance.Resolve(typeof(CouponsReportDatabase)) as CouponsReportDatabase;
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static AccountReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }
        public static AddressReportDatabase AddressDatabase
        {
            get { return _reportAddressDatabase; }
        }

        public static ShoppingCartReportDatabase ShppingCartDatabase
        {
            get { return _reportShppingCartDatabase; }
        }

        public static AuthenticationReportDatabase AuthenticationReportDatabase
        {
            get { return _authenticationReportDatabase; }
        }
        public static AssociatorReportDatabase AssociatorReportDatabase
        {
            get { return _associatorReportDatabase; }
        }
        public static SysKindReportDatabase KindReportDatabase
        {
            get { return _kindReportDatabase; }
        }
        public static AccountInfoReportDatabase AccountInfoReportDatabase
        {
            get { return _accountInfoReportDatabase; }
        }

        public static TradeReportDatabase TradeReportDatabase
        {
            get { return _tradeReportDatabase; }
        }

        public static CouponsReportDatabase CouponsReportDatabase
        {
            get { return _couponsReportDatabase; }
        }
    }
}
