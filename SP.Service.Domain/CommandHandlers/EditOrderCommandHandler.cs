using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Data.Enum;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class EditOrderCommandHandler : ICommandHandler<EditOrderCommand>,ICommandHandler<EditOrderCodeCommand>
    {
        private IDataRepository<OrderDomain> _repository;
        private IDataRepository<TradeDomain> _tradeRepository;
        private IDataRepository<AccountFinanceDomain> _financeRepository;
        private IDataRepository<ProductSkuDomain> _skuRepository;
        private OrderReportDatabase _orderReportDatabase;
        private AccountFinanceReportDatabase _financeReportDatabase;

        public EditOrderCommandHandler(IDataRepository<OrderDomain> repository, IDataRepository<TradeDomain> tradeRepository,
            IDataRepository<AccountFinanceDomain> financeRepository, OrderReportDatabase orderReportDatabase, 
            IDataRepository<ProductSkuDomain> skuRepository, AccountFinanceReportDatabase financeReportDatabase)
        {
            this._repository = repository;
            this._tradeRepository = tradeRepository;
            this._financeRepository = financeRepository;
            this._orderReportDatabase = orderReportDatabase;
            this._skuRepository = skuRepository;
            this._financeReportDatabase = financeReportDatabase;
        }

        public void Execute(EditOrderCommand command)
        {
            var aggregate = new OrderDomain();
            aggregate.EditOrderDomainStatus(command.Id, command.OrderStatus);
            _repository.Save(aggregate);
            
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderId(command.Id.ToString());
            CaclCommsion(order, command.OrderStatus);
        }

        public void Execute(EditOrderCodeCommand command)
        {
            var order = _orderReportDatabase.GetLeadOrderDomainByOrderCode(command.OrderCode);
            if (order != null)
            {
                var aggregate = new OrderDomain();
                aggregate.EditOrderDomainStatus(new Guid(order.OrderId), command.OrderStatus);
                _repository.Save(aggregate);

                CaclCommsion(order, command.OrderStatus);
            }
            else
            {
                throw new OrderCodeUpdateException(command.OrderCode);
            }
        }
        private void CaclCommsion(LeadOrderDomain order, OrderStatus orderStatus)
        {
            double commsion = 0;
            if (order != null && order.Shop != null && !string.IsNullOrEmpty(order.Shop.OwnerId)
                && order.ShoppingCarts != null)
            {
                System.Console.WriteLine("orderStatus=" + orderStatus);
                if (orderStatus == Data.Enum.OrderStatus.Success)
                {
                    foreach (var cart in order.ShoppingCarts)
                    {
                        if (cart != null && !string.IsNullOrEmpty(cart.CartId))
                        {
                            var amount = cart.Quantity * 0.5;
                            var trade = new TradeDomain(order.Shop.OwnerId, cart.CartId, 1, amount);
                            _tradeRepository.Save(trade);
                            commsion += amount;
                        }
                    }
                    var finance = _financeReportDatabase.GetAccountFinanceDetail(order.Shop.OwnerId);
                    if (finance != null && !string.IsNullOrEmpty(finance.AccountId))
                    {
                        var financeDomain = new AccountFinanceDomain();
                        financeDomain.EditHaveAmount(finance.AccountId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                    else
                    {
                        var financeDomain = new AccountFinanceDomain(order.Shop.OwnerId, commsion);
                        _financeRepository.Save(financeDomain);
                    }
                }
                else if (orderStatus == Data.Enum.OrderStatus.Payed)
                {
                    foreach (var cart in order.ShoppingCarts)
                    {
                        if (cart != null && !string.IsNullOrEmpty(cart.CartId))
                        {
                            System.Console.WriteLine("EditProductSkuDomainStock Quantity=" + cart.Quantity);
                            var sku = new ProductSkuDomain();
                            //sku.EditProductSkuDomainStock(cart.Product.ProductId, cart.Quantity);
                            sku.EditProductSkuOrderNum(cart.ShopId,cart.Product.ProductId, cart.Quantity);
                            _skuRepository.Save(sku);
                        }
                    }
                }
            }
        }
    }
}
