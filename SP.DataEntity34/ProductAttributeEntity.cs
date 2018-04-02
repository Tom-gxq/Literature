using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_ProductAttribute")]
    public class ProductAttributeEntity : Entity
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
        public string ProductId { get; set; }
        public long? AttributeId { get; set; }
        public long? ValueId { get; set; }
    }
}
