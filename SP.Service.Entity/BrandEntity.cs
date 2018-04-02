using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_BrandCategories")]
    public class BrandEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string BrandName { get; set; }
        public string Logo { get; set; }
        public string CompanyUrl { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string Description { get; set; }
        public int? DisplaySequence { get; set; }

    }
}
