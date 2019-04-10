using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class CouponsReportDatabase : IReportDatabase
    {
        private readonly CouponsRepository _repository;
        public CouponsReportDatabase(CouponsRepository repository)
        {
            _repository = repository;
        }
        public bool Add(CouponsEntity entity)
        {
            return _repository.Add(entity);
        }

        public bool Update(CouponsEntity entity)
        {
            return _repository.Update(entity);
        }

        public List<CouponsDomain> GetAccountCouponsList(string accountId)
        {
            var entityList = _repository.GetAccountCouponsList(accountId);
            var list = new List<CouponsDomain>();
            foreach (var item in entityList)
            {
                var domain = new CouponsDomain();
                domain.SetMemento(item);
                list.Add(domain);
            }
            return list;
        }

        private AssociatorDomain ConvertEntityToDomain(AssociatorEntity entity)
        {
            var associator = new AssociatorDomain();
            associator.SetMemento(entity);
            return associator;
        }
    }
}
