using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.AutoMap
{
    class GuidTypeConverter : ITypeConverter<int, Guid>
    {
        public Guid Convert(int source, Guid destination, ResolutionContext context)
        {
            return Guid.NewGuid();
        }
    }
}
