﻿using Grpc.Service.Core.Domain.Reporting;
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
        public SysKindReportDatabase(SysKindRepository repository)
        {
            _repository = repository;
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
    }
}
