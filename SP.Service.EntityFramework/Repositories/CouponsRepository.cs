using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class CouponsRepository : EfRepository<CouponsEntity>
    {
        public CouponsRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public bool Add(CouponsEntity entity)
        {
            var result = this.Insert(entity);
            return result > 0;
        }
        public bool Update(CouponsEntity entity)
        {
            var result = this.UpdateNonDefaults(entity, x => x.CouponId == entity.CouponId);
            return result > 0;
        }
        public List<CouponsFullEntity> GetAccountCouponsList(string accountId)
        {
            var nowDate = DateTime.Parse(DateTime.Now.ToShortDateString());
            using (var db = OpenDbConnection())
            {
                var q = db.From<CouponsEntity>();
                q = q.Join<CouponsEntity, SysKindEntity>((e, a) => e.AccountId == accountId && e.Status == 1 && e.PayStatus == 1 && a.KindId == e.KindId );
                q = q.Join<SysKindEntity, SaleModeEntity>((e, a) => e.SaleModeId == a.SaleModeId);
                return db.Select<CouponsFullEntity>(q);
            }
        }
        public List<CouponsFullEntity> GetCouponByCode(string couponCode)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<CouponsEntity>();
                q = q.Join<CouponsEntity, SysKindEntity>((e, a) => e.PayOrderCode == couponCode && a.KindId == e.KindId);
                q = q.Join<SysKindEntity, SaleModeEntity>((e, a) => e.SaleModeId == a.SaleModeId);
                return db.Select<CouponsFullEntity>(q);
            }
        }
        public List<CouponsFullEntity> GetCouponById(string couponId)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<CouponsEntity>();
                q = q.Join<CouponsEntity, SysKindEntity>((e, a) => e.CommandId == couponId && a.KindId == e.KindId);
                return db.Select<CouponsFullEntity>(q);
            }
        }
    }
}
