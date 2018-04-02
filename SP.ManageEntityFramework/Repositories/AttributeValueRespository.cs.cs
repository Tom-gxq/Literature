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
    public class AttributeValueRespository: RepositoryBase<AttributeValueEntity, int>
    {
        public AttributeValueRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<AttributeValueEntity> GetAttributeValueList(int attributeId)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<AttributeValueEntity>().Where(x=>x.AttributeId == attributeId).OrderBy(x => x.DisplaySequence);
                return db.Select(q);
            }
        }

        public bool DeleteAttributeValue(int valueId)
        {
            bool result = false;
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    db.Delete<AttributeValueEntity>(x=>x.Id == valueId);
                }
                result = true;
            }
            catch
            {

            }
            return result;
        }


    }
}
