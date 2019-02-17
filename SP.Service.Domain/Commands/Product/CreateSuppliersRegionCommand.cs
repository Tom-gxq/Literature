using Grpc.Service.Core.Domain.Commands;
using SP.Service.Domain.Util;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Commands.Product
{
    public class CreateSuppliersRegionCommand : SPCommand
    {
        public int SuppliersId { get; set; }
        public int RegionId { get; set; }
        public CreateSuppliersRegionCommand(int suppliersId, int regionId) 
            : base(KafkaConfig.NormalCommandBusTopicTitle)
        {            
            this.RegionId = regionId;
            this.SuppliersId = suppliersId;
            this.CommandType = CommandType.CreateSuppliersRegion;
        }
    }
}
