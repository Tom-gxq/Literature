using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class SysKindRepository : EfRepository<SysKindEntity>
    {
        public SysKindRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public SysKindEntity GetSysKindById(string kindId)
        {
            return this.Single(x => x.KindId == kindId);
        }
        public List<SysKindEntity> GetSysKind(int kind)
        {
            return this.Select(x=>x.Kind == kind && x.IsDisplay ==true);
        }
    }
}
