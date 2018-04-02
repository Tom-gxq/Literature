using LibMain.Domain.Entities;
using ServiceStack.DataAnnotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.DataEntity
{
    public class EventFullInfoEntity : Entity
    {
        public int SysEventId { get; set; }
        [Alias("SP_SysEvent.EventName")]
        public string SysEventName { get; set; }
        public int ResEventId { get; set; }
        [Alias("SP_ResEvent.EventName")]
        public string ResEventName { get; set; }
        public int Quantity { get; set; }
        public string KindId { get; set; }
        public string Description { get; set; }
    }
}
