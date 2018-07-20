using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Messaging;
using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Order.Service.Business
{
    public sealed class ServiceLocator
    {
        private static ICommandBus _commandBus;
        private static OrderReportDatabase _reportDatabase;
        private static AssociatorReportDatabase _associatorReportDatabase;
        private static TradeReportDatabase _tradeReportDatabase;
        private static AccountFinanceReportDatabase _financeReportDatabase;
        private static CashApplyReportDatabase _cashApplyReportDatabase;
        private static bool _isInitialized;
        private static readonly object _lockThis = new object();

        static ServiceLocator()
        {
            if (!_isInitialized)
            {
                lock (_lockThis)
                {
                    _commandBus = IocManager.Instance.Resolve(typeof(ICommandBus)) as ICommandBus;
                    _reportDatabase = IocManager.Instance.Resolve(typeof(OrderReportDatabase)) as OrderReportDatabase;
                    _associatorReportDatabase = IocManager.Instance.Resolve(typeof(AssociatorReportDatabase)) as AssociatorReportDatabase;
                    _tradeReportDatabase = IocManager.Instance.Resolve(typeof(TradeReportDatabase)) as TradeReportDatabase;
                    _financeReportDatabase = IocManager.Instance.Resolve(typeof(AccountFinanceReportDatabase)) as AccountFinanceReportDatabase;
                    _cashApplyReportDatabase = IocManager.Instance.Resolve(typeof(CashApplyReportDatabase)) as CashApplyReportDatabase;
                    _isInitialized = true;
                }
            }
        }
        public static ICommandBus CommandBus
        {
            get { return _commandBus; }
        }
        public static OrderReportDatabase ReportDatabase
        {
            get { return _reportDatabase; }
        }
        public static AssociatorReportDatabase AssociatorReportDatabase
        {
            get { return _associatorReportDatabase; }
        }
        public static TradeReportDatabase TradeReportDatabase
        {
            get { return _tradeReportDatabase; }
        }
        public static AccountFinanceReportDatabase FinanceReportDatabase
        {
            get { return _financeReportDatabase; }
        }
        public static CashApplyReportDatabase CashApplyReportDatabase
        {
            get { return _cashApplyReportDatabase; }
        }
    }
}
