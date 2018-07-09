using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class ShipOrderReportDatabase : IReportDatabase
    {
        private readonly ShipOrderRespository _repository;
        public ShipOrderReportDatabase(ShipOrderRespository repository)
        {
            _repository = repository;
        }

        public void Add(ShippingOrdersEntity item)
        {
            _repository.Add(item);
        }

        public List<ShippingOrdersEntity> GetShippingOrdersByOrderId(string orderId)
        {
            return _repository.GetShippingOrdersByOrderId(orderId);
        }
    }
}
