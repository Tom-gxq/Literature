using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Events
{
    public class AliBindEvent: BaseBindEvent
    {
        public AliBindEvent(Guid id, string otherAccount):base(id, otherAccount)
        {
            
        }
    }
}
