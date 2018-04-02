using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductImage")]
    public class ProductImageEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string ProductId { get; set; }
        public string ImgPath { get; set; }
        public int? Postion { get; set; }
        public int? DisplaySequence { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool? IsDel { get; set; }
    }
}
