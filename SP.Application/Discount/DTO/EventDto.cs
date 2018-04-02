using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP.Application.Discount.DTO
{
    public class EventDto
    {
        public int Id { get; set; }
        public int SysEventId { get; set; }
        public string SysEventName { get; set; }
        public int ResEventId { get; set; }
        public string ResEventName { get; set; }
        public int Quantity { get; set; }
        public string KindId { get; set; }
        public string Description { get; set; }
    }
}
