using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SysKindReportDatabase : IReportDatabase
    {
        private readonly SysKindRepository _repository;
        private readonly CouponsRepository _couponRepository;
        public SysKindReportDatabase(SysKindRepository repository, CouponsRepository couponRepository)
        {
            _repository = repository;
            _couponRepository = couponRepository;
        }
        public SysKindDomain GetSysKindById(string kindId)
        {
            var entity = _repository.GetSysKindById(kindId);
            var domain = new SysKindDomain();
            domain.SetMemento(entity);
            return domain;
        }

        public List<SysKindDomain> GetSysKind(int kind)
        {
            var entityList = _repository.GetSysKind(kind);
            var list = new List<SysKindDomain>();
            foreach (var item in entityList)
            {
                var domain = new SysKindDomain();
                domain.SetMemento(item);
                list.Add(domain);
            }
            return list;
        }

        public List<CouponsDomain> GetAccountCouponsList(string accountId)
        {
            var entityList = _couponRepository.GetAccountCouponsList(accountId);
            var list = new List<CouponsDomain>();
            foreach (var item in entityList)
            {
                var domain = new CouponsDomain();
                domain.SetMemento(item);
                list.Add(domain);
            }
            return list;
        }
    }
}
