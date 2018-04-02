using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataEntity
{
    [Alias("SP_AccountInfo")]
    public class AccountInfoEntity : Entity
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
        public string Avatar { get; set; }
        public string Fullname { get; set; }
        public int? UserType { get; set; }
        public int? Gender { get; set; }
        public string IM_QQ { get; set; }
        public string WeiXin { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
