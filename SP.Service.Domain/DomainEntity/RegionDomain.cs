using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.DomainEntity
{
    public class RegionDomain : AggregateRoot<int>,
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
        public int CityID { get; set; }
        public int ProvinceID { get; set; }
        public RegionDomain()
        {

        }

        public BaseEntity GetMemento()
        {
            return new RegionEntity()
            {
                DataID = this.DataID,
                DataName = this.DataName,
                ParentDataID = this.ParentDataID,
            };
        }


        public void SetMemento(BaseEntity memento)
        {
            if (memento is RegionEntity)
            {
                var entity = memento as RegionEntity;
                this.DataID = entity.DataID;
                this.DataName = entity.DataName;
                this.ParentDataID = entity.BuiddingID;
            }
        }
    }
}
