using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_AccountProduct")]
    public class AccountProductEntity : Entity
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
        public string ProductId { get; set; }
        public int? ShopId { get; set; }
        public int? PreStock { get; set; }
        public int? Status { get; set; }
    }
}
