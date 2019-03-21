using Grpc.Service.Core.Domain.Reporting;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class BalancePayReportDatabase : IReportDatabase
    {
        private readonly BalancePayRepository _repository;
        public BalancePayReportDatabase(BalancePayRepository repository)
        {
            _repository = repository;
        }
        public void Pay(string accountId, string orderCode, string password, double amount, string tradeId,string sign)
        {
            _repository.BalancePay( accountId,  orderCode,  password,  amount, tradeId, sign);
        }
    }
}
