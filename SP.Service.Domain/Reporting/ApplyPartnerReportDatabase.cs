using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ApplyPartnerReportDatabase : IReportDatabase
    {
        private readonly ApplyPartnerRepository _repository;
        public ApplyPartnerReportDatabase(ApplyPartnerRepository repository)
        {
            _repository = repository;
        }

        public bool Add(ApplyPartnerEntity item)
        {
            return _repository.Add(item);
        }
    }
}
