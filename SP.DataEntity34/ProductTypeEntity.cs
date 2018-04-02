using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_ProductType")]
    public class ProductTypeEntity : Entity
    {
        [AutoIncrement]
        public override int Id
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
        public string TypeName { get; set; }
        public int? Kind { get; set; }
        public string Remark { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
