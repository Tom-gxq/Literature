using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Sender
{
    public class AbstractEntity
    {
        public bool IsSuccess = false;
        public int ErrTimes = 0;
        public string ID { get; set; }

        public virtual void Run()
        {

        }
        public override bool Equals(object obj)
        {
            AbstractEntity item = obj as AbstractEntity;
            if (!string.IsNullOrEmpty(item.ID) && !string.IsNullOrEmpty(this.ID))
                return item.ID.ToLower() == this.ID.ToLower();
            else
                return false;
        }
    }
}
