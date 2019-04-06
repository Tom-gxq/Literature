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
        public ShippingOrdersEntity GetShippingOrdersById(int shipOrderId)
        {
            return _repository.GetShippingOrdersById(shipOrderId);
        }
        public ShippingOrdersEntity GetShippingOrders(string orderId,string accountId, string productId)
        {
            return _repository.GetShippingOrders(orderId, accountId, productId);
        }

        public ShippingOrdersEntity GetShippingOrders(string orderId, string accountId)
        {
            return _repository.GetShippingOrders(orderId, accountId);
        }

        public bool UpdateShippingOrderStatus(ShippingOrdersEntity item)
        {
            return _repository.UpdateShippingOrderStatus(item);
        }

        public List<ShippingOrdersEntity> GetTodayShippingOrders(string accountId, string productId)
        {
            return _repository.GetTodayShippingOrders( accountId, productId);
        }
    }
}
