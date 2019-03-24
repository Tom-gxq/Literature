using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Newtonsoft.Json;
using SP.MongoDB.Repositories;
using SP.Service.Domain.Events;
using SP.Service.Domain.Exceptions;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.EventHandlers.Execute
{
    public class EventExecuteHandler : IEventExecuteHandler
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;
        private readonly EventMongoDbRepository _reportDatabase;
        public EventExecuteHandler(IEventHandlerFactory eventHandlerFactory, EventMongoDbRepository reportDatabase)
        {
            _eventHandlerFactory = eventHandlerFactory;
            _reportDatabase = reportDatabase;
        }
        public void ExecuteEvent(string text)
        {
            var @event = JsonConvert.DeserializeObject<Event>(text);
            switch (@event.EventType)
            {
                case EventType.AccessTokenCreated:
                    ExecuteEvent<AccessTokenCreatedEvent>(text);
                    break;
                case EventType.AccountCreated:
                    ExecuteEvent<AccountCreatedEvent>(text);
                    break;
                case EventType.AccountEdit:
                    ExecuteEvent<AccountEditEvent>(text);
                    break;
                case EventType.AccountFinanceCreate:
                    ExecuteEvent<AccountFinanceCreateEvent>(text);
                    break;
                case EventType.AccountInfoCreated:
                    ExecuteEvent<AccountInfoCreatedEvent>(text);
                    break;
                case EventType.AccountInfoEdit:
                    ExecuteEvent<AccountInfoEditEvent>(text);
                    break;
                case EventType.AccountPayPwdCreate:
                    ExecuteEvent<AccountPayPwdCreateEvent>(text);
                    break;
                case EventType.AccountPayPwdEdit:
                    ExecuteEvent<AccountPayPwdEditEvent>(text);
                    break;
                case EventType.AddressCreated:
                    ExecuteEvent<AddressCreatedEvent>(text);
                    break;
                case EventType.AddressEdit:
                    ExecuteEvent<AddressEditEvent>(text);
                    break;
                case EventType.AddShoppingCartNum:
                    ExecuteEvent<AddShoppingCartNumEvent>(text);
                    break;
                case EventType.ApplyPartnerCreated:
                    ExecuteEvent<ApplyPartnerCreatedEvent>(text);
                    break;
                case EventType.AssociatorCreated:
                    ExecuteEvent<AssociatorCreatedEvent>(text);
                    break;
                case EventType.AssociatorEdit:
                    ExecuteEvent<AssociatorEditEvent>(text);
                    break;
                case EventType.AuthenticationCreated:
                    ExecuteEvent<AuthenticationCreatedEvent>(text);
                    break;
                case EventType.AuthenticationEdit:
                    ExecuteEvent<AuthenticationEditEvent>(text);
                    break;
                case EventType.CashApplyCreated:
                    ExecuteEvent<CashApplyCreatedEvent>(text);
                    break;
                case EventType.CreatShoppingCart:
                    ExecuteEvent<CreatShoppingCartEvent>(text);
                    break;
                case EventType.DecreaseProductSku:
                    ExecuteEvent<DecreaseProductSkuEvent>(text);
                    break;
                case EventType.DelAddress:
                    ExecuteEvent<DelAddressEvent>(text);
                    break;
                case EventType.DelShoppingCart:
                    ExecuteEvent<DelShoppingCartEvent>(text);
                    break;
                case EventType.EditShipOrderStatus:
                    ExecuteEvent<EditShipOrderStatusEvent>(text);
                    break;
                case EventType.HaveAmountEdit:
                    ExecuteEvent<HaveAmountEditEvent>(text);
                    break;
                case EventType.KafkaAdd:
                    ExecuteEvent<KafkaAddEvent>(text);
                    break;
                case EventType.OrderCreated:
                    ExecuteEvent<OrderCreatedEvent>(text);
                    break;
                case EventType.OrderEdit:
                    ExecuteEvent<OrderEditEvent>(text);
                    break;
                case EventType.OrderProductCreated:
                    ExecuteEvent<OrderProductCreatedEvent>(text);
                    break;
                case EventType.OrderStatisticsCreate:
                    ExecuteEvent<OrderStatisticsCreateEvent>(text);
                    break;
                case EventType.OrderStatisticsSum:
                    ExecuteEvent<OrderStatisticsSumEvent>(text);
                    break;
                case EventType.OrderSubAmount:
                    ExecuteEvent<OrderSubAmountEvent>(text);
                    break;
                case EventType.ProductCreated:
                    ExecuteEvent<ProductCreatedEvent>(text);
                    break;
                case EventType.ProductDel:
                    ExecuteEvent<ProductDelEvent>(text);
                    break;
                case EventType.ProductEdit:
                    ExecuteEvent<ProductEditEvent>(text);
                    break;
                case EventType.ProductImageCreated:
                    ExecuteEvent<ProductImageCreatedEvent>(text);
                    break;
                case EventType.ProductImageEdit:
                    ExecuteEvent<ProductImageEditEvent>(text);
                    break;
                case EventType.ProductSkuDBCreate:
                    ExecuteEvent<ProductSkuDBCreateEvent>(text);
                    break;
                case EventType.ProductSkuDBUpdate:
                    ExecuteEvent<ProductSkuDBUpdateEvent>(text);
                    break;
                case EventType.ProductSkuEdit:
                    ExecuteEvent<ProductSkuEditEvent>(text);
                    break;
                case EventType.ProductSkuOrderNum:
                    ExecuteEvent<ProductSkuOrderNumEvent>(text);
                    break;
                case EventType.RedoProductSku:
                    ExecuteEvent<RedoProductSkuEvent>(text);
                    break;
                case EventType.ResidueSkuUpdate:
                    ExecuteEvent<ResidueSkuUpdateEvent>(text);
                    break;
                case EventType.SaleStatusEdit:
                    ExecuteEvent<SaleStatusEditEvent>(text);
                    break;
                case EventType.SellerStatistics:
                    ExecuteEvent<SellerStatisticsEvent>(text);
                    break;
                case EventType.SellerStatisticsSumOrder:
                    ExecuteEvent<SellerStatisticsSumOrderEvent>(text);
                    break;
                case EventType.SellerStatisticsTrade:
                    ExecuteEvent<SellerStatisticsTradeEvent>(text);
                    break;
                case EventType.ShipOrderCreated:
                    ExecuteEvent<ShipOrderCreatedEvent>(text);
                    break;
                case EventType.ShipOrderEdit:
                    ExecuteEvent<ShipOrderEditEvent>(text);
                    break;
                case EventType.SysStatisticsCreate:
                    ExecuteEvent<SysStatisticsCreateEvent>(text);
                    break;
                case EventType.SysStatisticsSumBuyMember:
                    ExecuteEvent<SysStatisticsSumBuyMemberEvent>(text);
                    break;
                case EventType.SysStatisticsSumNewMember:
                    ExecuteEvent<SysStatisticsSumNewMemberEvent>(text);
                    break;
                case EventType.SysStatisticsSumOrder:
                    ExecuteEvent<SysStatisticsSumOrderEvent>(text);
                    break;
                case EventType.SysStatisticsSumUser:
                    ExecuteEvent<SysStatisticsSumUserEvent>(text);
                    break;
                case EventType.TokenCreated:
                    ExecuteEvent<TokenCreatedEvent>(text);
                    break;
                case EventType.TokenDisabled:
                    ExecuteEvent<TokenDisabledEvent>(text);
                    break;
                case EventType.TradeCreate:
                    ExecuteEvent<TradeCreateEvent>(text);
                    break;
                case EventType.UpdateShoppingCartOrderID:
                    ExecuteEvent<UpdateShoppingCartOrderIDEvent>(text);
                    break;
                case EventType.UseAmountEdit:
                    ExecuteEvent<UseAmountEditEvent>(text);
                    break;
                case EventType.BalancePay:
                    ExecuteEvent<BalancePayEvent>(text);
                    break;
                case EventType.ConsumeTradeCreate:
                    ExecuteEvent<ConsumeTradeCreateEvent>(text);
                    break;
                case EventType.IncomeTradeCreate:
                    ExecuteEvent<IncomeTradeCreateEvent>(text);
                    break;
                case EventType.WxOpenIdCreate:
                    ExecuteEvent<WxOpenIdCreateEvent>(text);
                    break;
                case EventType.WxUnionIdEdit:
                    ExecuteEvent<WxUnionIdEditEvent>(text);
                    break;
                case EventType.SuppliersProductCreated:
                    ExecuteEvent<SuppliersProductCreatedEvent>(text);
                    break;
                case EventType.SuppliersRegionCreated:
                    ExecuteEvent<SuppliersRegionCreatedEvent>(text);
                    break;
                case EventType.SellerProductCreated:
                    ExecuteEvent<SellerProductCreatedEvent>(text);
                    break;
                case EventType.SellerProductDel:
                    ExecuteEvent<SellerProductDelEvent>(text);
                    break;
                default:
                    throw new UnregisteredDomainCommandException($" unknown event: [{text}]");

            }
        }
        private void ExecuteEvent<T>(string text) where T : Event
        {
            var @event = JsonConvert.DeserializeObject<T>(text);
            SaveEvent<T>(@event);
            var handlers = _eventHandlerFactory.GetHandlers<T>();
            if (handlers != null && @event != null)
            {
                foreach (var eventHandler in handlers)
                {
                    eventHandler.Handle(@event);
                }
                UpdateEventStatus<T>(@event.AggregateId.ToString(),1);
            }
            else
            {
                throw new UnregisteredDomainCommandException($"no handler registered or unknown event: [{text}]");
            }
        }

        private void SaveEvent<T>(T @event) where T : Event
        {
            @event.EventId = @event.AggregateId.ToString();
            this._reportDatabase.Insert(@event);
        }
        private void UpdateEventStatus<T>(string eventId,int status) where T : Event
        {
            this._reportDatabase.UpdateEventExcuteStatus(eventId, status);
        }
    }
}
