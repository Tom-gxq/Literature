using Grpc.Service.Core.Domain.Entity;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Entity
{
    [Alias("SP_Suppliers")]
    public class SuppliersEntity : BaseEntity
    {
        [AutoIncrement]
        public int? Id { get; set; }
        public string SuppliersName { get; set; }
        public string AccountId { get; set; }
        public string LogoPath { get; set; }
        public string LicensePath { get; set; }
        public string PermitPath { get; set; }
        public string AuthorizationPath { get; set; }
        public string TelPhone { get; set; }
        public string AlipayNo { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
        public int? TypeId { get; set; }
    }
}
