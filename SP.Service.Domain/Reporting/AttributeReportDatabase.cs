using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class AttributeReportDatabase : IReportDatabase
    {
        private readonly AttributeRepository _repository;
        public AttributeReportDatabase(AttributeRepository repository)
        {
            _repository = repository;
        }

        public List<AttributeDomain> GetTitleAttributeList(int attType)
        {
            var addressDomainList = new List<AttributeDomain>();
            var addressList = _repository.GetTitleAttributeList(attType);
            foreach (var item in addressList)
            {
                var order = ConvertAttributeEntityToDomain(item);
                addressDomainList.Add(order);
            }
            return addressDomainList;
        }

        private AttributeDomain ConvertAttributeEntityToDomain(AttributeEntity entity)
        {
            var account = new AttributeDomain();
            account.SetMemento(entity);
            return account;
        }
    }
}
