using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_Account")]
    public class AccountEntity : Entity
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
        public string AccountId { get; set; }
        public string MobilePhone { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int? Status { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
