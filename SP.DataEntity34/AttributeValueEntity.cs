using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_AttributeValue")]
    public class AttributeValueEntity : Entity
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

        public int? AttributeId { get; set; }
        public int? DisplaySequence { get; set; }
        public string ValueStr { get; set; }
        public string ImageUrl { get; set; }
    }
}
