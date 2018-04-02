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
    public class AttributeRespository : RepositoryBase<AttributeEntity, int>
    {
        public AttributeRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public List<AttributeEntity> GetAttributeList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AttributeEntity>().OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public List<AttributeEntity> SearchAttributeByName(string name, int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AttributeEntity>().Where(x => x.AttributeName.Contains(name)).OrderBy(x => x.DisplaySequence);
                q = q.Limit((pageIndex - 1) * pageSize, pageSize);
                return db.Select(q);
            }
        }

        public int SearchAttributeByNameCount(string name)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AttributeEntity>().Where(x => x.AttributeName.Contains(name));
                return db.Select(q).Count();
            }
        }

        public bool DeleteAttribute(int attributeId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var result = db.Delete<AttributeEntity>(x => x.Id == attributeId);
                return result>0;
            }
        }
    }
}
