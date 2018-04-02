using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_Attribute")]
    public class AttributeEntity : Entity
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
        public string AttributeName { get; set; }

        public int? DisplaySequence { get; set; }

        public string UseAttributeImage { get; set; }
    }
}
