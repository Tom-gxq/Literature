using Grpc.Service.Core.Domain.Repositories;
using ServiceStack.OrmLite;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.EntityFramework.Repositories
{
    public class EventRepository : EfRepository<EventRelationEntity>
    {
        public EventRepository(DataContext context) : base(context)
        {
            DbConnection = context.GetConnectionString();
        }
        public List<EventFullInfoEntity> GetDefaultEventList(int eventType)
        {
            using (var db = OpenDbConnection())
            {
                var q = db.From<EventRelationEntity>();
                q = q.Join<EventRelationEntity, SysEventEntity>((a, b) => a.SysEventId == b.ID && b.EventType == eventType);
                q = q.Join<EventRelationEntity, ResEventEntity>((a, c) => a.ResEventId == c.ID);
                q = q.Join<EventRelationEntity, SysKindEntity>((a, d) => a.KindId == d.KindId);
                q = q.Select<EventRelationEntity, SysEventEntity, ResEventEntity, SysKindEntity>((a, b, c, d) => new { a, SysEventName = b.EventName, ResEventName = c.EventName, d.Description });
                return db.Select<EventFullInfoEntity>(q);
            }
        }
    }
}
