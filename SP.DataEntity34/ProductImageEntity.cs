using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_ProductImage")]
    public class ProductImageEntity : Entity
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
        public string ImgPath { get; set; }
        public int? Postion { get; set; }
        public int? DisplaySequence { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
