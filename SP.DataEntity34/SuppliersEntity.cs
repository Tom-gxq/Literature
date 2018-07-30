﻿using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_Suppliers")]
    public class SuppliersEntity : Entity
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
        public int? Type { get; set; }
    }
}
