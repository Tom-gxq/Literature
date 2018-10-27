using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.AutoMapper
{
    public static class AutoMapperExtensions
    {
        public static T ToModel<T>(this object entity)
        {
            return Mapper.Map<T>(entity);
        }

        public static T ToDomain<T>(this object entity)
        {
            return Mapper.Map<T>(entity);
        }

        public static T ToEntity<T>(this object entity)
        {
            return Mapper.Map<T>(entity);
        }
    }
}
