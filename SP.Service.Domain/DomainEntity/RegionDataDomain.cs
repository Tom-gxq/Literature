using Grpc.Service.Core.Domain;
using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class RegionDataDomain : AggregateRoot<int>,
        IOriginator
    {
        public int DataID { get; set; }
        /**
        *  区域名称
        */
        public string DataName { get; set; }
        /**
        *  区域父ID
        */
        public int ParentDataID { get; set; }

        public int DataType { get; set; }

        public int Status { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public RegionDataDomain()
        {

        }

        public BaseEntity GetMemento()
        {
            return new RegionDataEntity()
            {
                DataID = this.DataID,
                DataName = this.DataName,
                ParentDataID = this.ParentDataID,
                DataType = this.DataType,
                Status = this.Status,
                UpdateTime = this.UpdateTime,
                CreateTime = this.CreateTime
            };
        }
        

        public void SetMemento(BaseEntity memento)
        {
            if (memento is RegionDataEntity)
            {
                var entity = memento as RegionDataEntity;
                this.DataID = entity.DataID;
                this.DataName = entity.DataName;
                this.ParentDataID = entity.ParentDataID.Value;
                this.DataType = entity.DataType.Value;
                this.Status = entity.Status.Value;
                this.CreateTime = entity.CreateTime.Value;
                this.UpdateTime = entity.UpdateTime.Value;
            }
        }
    }
}
