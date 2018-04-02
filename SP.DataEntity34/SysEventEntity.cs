using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    [Alias("SP_SysEvent")]
    public class SysEventEntity : Entity
    {
        [AutoIncrement]
        [Alias("Id")]
        public override int Id { get; set; }
        public string EventName { get; set; }
        public int? EventType { get; set; }
    }
}
