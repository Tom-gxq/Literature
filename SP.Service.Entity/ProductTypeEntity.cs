using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_ProductType")]
    public class ProductTypeEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string TypeName { get; set; }
        public int? Kind { get; set; }
        public string Remark { get; set; }
        public string TypePath { get; set; }
        public string TypeLogo { get; set; }
        public int? DisplaySequence { get; set; }
    }
}
