using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_ResEvent")]
    public class ResEventEntity : Entity
    {
        [AutoIncrement]
        [Alias("Id")]
        public override int Id { get; set; }
        public string EventName { get; set; }
        public int? Kind { get; set; }
    }
}
