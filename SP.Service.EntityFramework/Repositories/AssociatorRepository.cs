using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class AssociatorRepository : EfRepository<AssociatorEntity>
    {
        public AssociatorRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }

        public bool Add(AssociatorEntity entity)
        {
            var result = this.Insert(entity);
            return result > 0;
        }
        public bool UpdateAssociator(AssociatorEntity entity)
        {
            var result = this.UpdateNonDefaults(entity,x=>x.AssociatorId == entity.AssociatorId);
            return result > 0;
        }
        public List<AssociatorEntity> GetAssociatorByAccountId(string accountId)
        {
            return this.Select(x=>x.AccountId == accountId && x.Status == 1 && x.EndDate >= DateTime.Now);
        }
        public AssociatorEntity GetAssociatorById(string associatorId)
        {
            return this.Single(x=>x.AssociatorId == associatorId);
        }
        public List<DiscountEntity> GetDiscountByAccountId(string accountId,int kind)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<AssociatorEntity>();
                q = q.Join<AssociatorEntity, SysKindEntity>((e, a) => e.AccountId == accountId && a.KindId == e.KindId && a.Kind == kind);                
                return db.Select<DiscountEntity>(q);
            }
        }
        public List<AssociatorEntity> GetMemberByAccountId(string accountId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<AssociatorEntity>();
                q = q.Join<AssociatorEntity, SysKindEntity>((e, a) => e.AccountId == accountId && e.Status == 1 && e.EndDate >= DateTime.Now && a.KindId == e.KindId && a.Kind == 100);
                return db.Select<AssociatorEntity>(q);
            }
        }
    }
}
