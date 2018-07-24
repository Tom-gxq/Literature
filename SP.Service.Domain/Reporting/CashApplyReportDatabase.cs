using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class CashApplyReportDatabase : IReportDatabase
    {
        private readonly CashApplyRepository _repository;
        public CashApplyReportDatabase(CashApplyRepository repository)
        {
            _repository = repository;
        }

        public bool Add(CashApplyEntity item)
        {
            return _repository.Add(item);
        }

        public double GetAllApplyNum(string accountId)
        {
            double total = 0;
            var list = _repository.GetAllApply(accountId);
            list.ForEach(x => total +=  x.Money );
            return total;
        }
    }
}
