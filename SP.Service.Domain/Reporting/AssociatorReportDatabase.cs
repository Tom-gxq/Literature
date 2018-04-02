using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AssociatorReportDatabase : IReportDatabase
    {
        private readonly AssociatorRepository _repository;
        public AssociatorReportDatabase(AssociatorRepository repository)
        {
            _repository = repository;
        }
        public bool Add(AssociatorEntity entity)
        {
            return _repository.Add(entity);
        }

        public bool UpdateAssociator(AssociatorEntity entity)
        {
            return _repository.UpdateAssociator(entity);
        }

        public List<AssociatorDomain> GetAssociatorByAccountId(string accountId)
        {
            var entityList = _repository.GetAssociatorByAccountId(accountId);
            var list = new List<AssociatorDomain>();
            foreach (var item in entityList)
            {
                var domain = new AssociatorDomain();
                domain.SetMemento(item);
                list.Add(domain);
            }
            return list;
        }
        public List<AssociatorDomain> GetMemberByAccountId(string accountId)
        {
            var entityList = _repository.GetMemberByAccountId(accountId);
            var list = new List<AssociatorDomain>();
            foreach (var item in entityList)
            {
                var domain = new AssociatorDomain();
                domain.SetMemento(item);
                list.Add(domain);
            }
            return list;
        }
        public AssociatorDomain GetAssociatorById(string associatorId)
        {
            var entity = _repository.GetAssociatorById(associatorId);
            
            var domain = new AssociatorDomain();
            domain.SetMemento(entity);

            return domain;
        }
        public List<DiscountDomain> GetDiscountByAccountId(string accountId, int kind)
        {
            var entityList = _repository.GetDiscountByAccountId(accountId, kind);
            var list = new List<DiscountDomain>();
            foreach (var item in entityList)
            {
                var domain = new DiscountDomain();
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
