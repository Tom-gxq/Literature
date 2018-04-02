using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;

namespace SP.DataEntity
{
    [Alias("SP_Admin")]
    public class AdminEntity: Entity<long>
    {
        [AutoIncrement]
        public override long Id
        {
            get
            {
                return base.Id;
            }

            set
            {
                base.Id = value;
            }
        }
        public string UserName { get; set; }
        public string PassWord { get; set; }
        public bool IsDel { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
