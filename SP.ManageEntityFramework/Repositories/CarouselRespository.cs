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
    public class CarouselRespository : RepositoryBase<CarouselEntity, int>
    {
        public CarouselRespository(IDbContextProvider<ManageDbContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }
        public List<CarouselEntity> GetCarouselList(int pageIndex, int pageSize)
        {
            using (var db = Context.OpenDbConnection())
            {
                var q = db.From<CarouselEntity>();
                
                return db.Select<CarouselEntity>(q);
            }
        }
        public long GetCarouselListCount()
        {
            return this.Count();
        }
        public bool AddCarousel(CarouselEntity resEvent)
        {
            var result = this.Insert(resEvent);
            return result > 0;
        }
        public bool DeleteCarousel(int id)
        {
            var result = this.Delete(x => x.Id == id);
            return result > 0;
        }
    }
}
