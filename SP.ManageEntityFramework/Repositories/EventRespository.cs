using Lib.EntityFramework.EntityFramework;
using ServiceStack.OrmLite;
using SP.DataEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.ManageEntityFramework.Repositories
{
    public class EventRespository : RepositoryBase<EventRelationEntity, int>
    {
        public EventRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<EventFullInfoEntity> GetEventList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<EventRelationEntity>();
                q = q.Join<EventRelationEntity, SysEventEntity>((a,b)=>a.SysEventId == b.Id);
                q = q.Join<EventRelationEntity, ResEventEntity>((a, c) => a.ResEventId == c.Id);
                q = q.Join<EventRelationEntity, SysKindEntity>((a, d) => a.KindId == d.Id);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                q = q.Select<EventRelationEntity, SysEventEntity, ResEventEntity, SysKindEntity>((a, b,c,d) => new { a, SysEventName = b.EventName, ResEventName = c.EventName,d.Description });
                return db.Select<EventFullInfoEntity>(q);
            }
        }
        public long GetEventListCount()
        {
            return this.Count();
        }
        public bool AddEvent(EventRelationEntity resEvent)
        {
            var result = this.Insert(resEvent);
            return result > 0;
        }
        public bool DeleteEvent(int id)
        {
            var result = this.Delete(x => x.Id == id);
            return result > 0;
        }
        public List<EventFullInfoEntity> GetDefaultEventList(int eventType)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<EventRelationEntity>();
                q = q.Join<EventRelationEntity, SysEventEntity>((a, b) => a.SysEventId == b.Id && b.EventType == eventType);
                q = q.Join<EventRelationEntity, ResEventEntity>((a, c) => a.ResEventId == c.Id);
                q = q.Join<EventRelationEntity, SysKindEntity>((a, d) => a.KindId == d.Id);
                q = q.Select<EventRelationEntity, SysEventEntity, ResEventEntity, SysKindEntity>((a, b, c, d) => new { a, SysEventName = b.EventName, ResEventName = c.EventName, d.Description });
                return db.Select<EventFullInfoEntity>(q);
            }
        }
    }
}
