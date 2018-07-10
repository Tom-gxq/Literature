using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class ProductTypeDomain : AggregateRoot<int>,
        IOriginator
    {
        public int Id { get; set; }
        public string TypeName { get; set; }
        public int Kind { get; set; }
        public string Remark { get; set; }
        public string TypePath { get; set; }
        public string TypeLogo { get; set; }

        public ProductTypeDomain()
        {

        }
        public BaseEntity GetMemento()
        {
            return new ProductTypeEntity()
            {
                Id = this.Id,
                TypeName = this.TypeName,
                Kind = this.Kind,
                Remark = this.Remark,
                TypePath = this.TypePath,
                TypeLogo = this.TypeLogo,
            };
        }

        public void SetMemento(BaseEntity memento)
        {
            if (memento is ProductTypeEntity)
            {
                var entity = memento as ProductTypeEntity;
                this.Id = entity.Id.Value;
                this.TypeName = entity.TypeName;
                this.Kind = entity.Kind!= null ? entity.Kind.Value:0;
                this.Remark = entity.Remark;
                this.TypePath = entity.TypePath;
                this.TypeLogo = entity.TypeLogo;
            }
        }
    }
}
