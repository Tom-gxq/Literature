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

        public CouponsDomain GetCouponByCode(string couponCode)
        {
            var entityList = _repository.GetCouponByCode(couponCode);
            var domain = new CouponsDomain();
            foreach (var item in entityList)
            {                
                domain.SetMemento(item);
            }
            return domain;
        }

        public CouponsDomain GetCouponById(string couponId)
        {
            var entityList = _repository.GetCouponById(couponId);
            var domain = new CouponsDomain();
            foreach (var item in entityList)
            {
                domain.SetMemento(item);
            }
            return domain;
        }

        private AssociatorDomain ConvertEntityToDomain(AssociatorEntity entity)
        {
            var associator = new AssociatorDomain();
            associator.SetMemento(entity);
            return associator;
        }
    }
}
