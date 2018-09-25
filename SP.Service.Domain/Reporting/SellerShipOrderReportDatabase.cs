using Grpc.Service.Core.Domain.Reporting;
using SP.Service.Entity;
using SP.Service.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.Reporting
{
    public class SellerShipOrderReportDatabase : IReportDatabase
    {
        private readonly SellerShipOrderRespository _repository;
        public SellerShipOrderReportDatabase(SellerShipOrderRespository repository)
        {
            _repository = repository;
        }

        public bool Add(ShipOrderEntity entity)
        {
            return _repository.Add(entity);
        }

        public bool UpdateShipOrderStatus(ShipOrderEntity item)
        {
            return _repository.UpdateShipOrderStatus(item);
        }
        public List<ShipOrderEntity> GetShippingOrdersByOrderId(string orderId)
        {
            return _repository.GetShippingOrdersByOrderId(orderId);
        }
        public ShipOrderEntity GetShippingOrdersById(int shipOrderId)
        {
            return _repository.GetShippingOrdersById(shipOrderId);
        }
    }
}
