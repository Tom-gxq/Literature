using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Newtonsoft.Json;
using SP.Service.Domain.Events;
using SP.Service.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.DataService
{
    class EventExecuteHandler : IEventExecuteHandler
    {
        private readonly IEventHandlerFactory _eventHandlerFactory;
        public EventExecuteHandler(IEventHandlerFactory eventHandlerFactory)
        {
            _eventHandlerFactory = eventHandlerFactory;
        }
        public void ExecuteEvent(string text)
        {
            var @event = JsonConvert.DeserializeObject<Event>(text);
            Event data = null;
            IEnumerable<IEventHandler<Event>> handlers = null;
            switch (@event.EventType)
            {
                case EventType.AccessTokenCreated:
                    data = JsonConvert.DeserializeObject<AccessTokenCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccessTokenCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountCreated:
                    data = JsonConvert.DeserializeObject<AccountCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountEdit:
                    data = JsonConvert.DeserializeObject<AccountEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountFinanceCreate:
                    data = JsonConvert.DeserializeObject<AccountFinanceCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountFinanceCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountInfoCreated:
                    data = JsonConvert.DeserializeObject<AccountInfoCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountInfoCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountInfoEdit:
                    data = JsonConvert.DeserializeObject<AccountInfoEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountInfoEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountPayPwdCreate:
                    data = JsonConvert.DeserializeObject<AccountPayPwdCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountPayPwdCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AccountPayPwdEdit:
                    data = JsonConvert.DeserializeObject<AccountPayPwdEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AccountPayPwdEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AddressCreated:
                    data = JsonConvert.DeserializeObject<AddressCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AddressCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AddressEdit:
                    data = JsonConvert.DeserializeObject<AddressEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AddressEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AddShoppingCartNum:
                    data = JsonConvert.DeserializeObject<AddShoppingCartNumEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AddShoppingCartNumEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ApplyPartnerCreated:
                    data = JsonConvert.DeserializeObject<ApplyPartnerCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ApplyPartnerCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AssociatorCreated:
                    data = JsonConvert.DeserializeObject<AssociatorCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AssociatorCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AssociatorEdit:
                    data = JsonConvert.DeserializeObject<AssociatorEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AssociatorEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AuthenticationCreated:
                    data = JsonConvert.DeserializeObject<AuthenticationCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AuthenticationCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.AuthenticationEdit:
                    data = JsonConvert.DeserializeObject<AuthenticationEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<AuthenticationEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.CashApplyCreated:
                    data = JsonConvert.DeserializeObject<CashApplyCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<CashApplyCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.CreatShoppingCart:
                    data = JsonConvert.DeserializeObject<CreatShoppingCartEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<CreatShoppingCartEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.DecreaseProductSku:
                    data = JsonConvert.DeserializeObject<DecreaseProductSkuEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<DecreaseProductSkuEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.DelAddress:
                    data = JsonConvert.DeserializeObject<DelAddressEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<DelAddressEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.DelShoppingCart:
                    data = JsonConvert.DeserializeObject<DelShoppingCartEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<DelShoppingCartEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.EditShipOrderStatus:
                    data = JsonConvert.DeserializeObject<EditShipOrderStatusEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<EditShipOrderStatusEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.HaveAmountEdit:
                    data = JsonConvert.DeserializeObject<HaveAmountEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<HaveAmountEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.KafkaAdd:
                    data = JsonConvert.DeserializeObject<KafkaAddEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<KafkaAddEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderCreated:
                    data = JsonConvert.DeserializeObject<OrderCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderEdit:
                    data = JsonConvert.DeserializeObject<OrderEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderProductCreated:
                    data = JsonConvert.DeserializeObject<OrderProductCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderProductCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderStatisticsCreate:
                    data = JsonConvert.DeserializeObject<OrderStatisticsCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderStatisticsCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderStatisticsSum:
                    data = JsonConvert.DeserializeObject<OrderStatisticsSumEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderStatisticsSumEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.OrderSubAmount:
                    data = JsonConvert.DeserializeObject<OrderSubAmountEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<OrderSubAmountEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductCreated:
                    data = JsonConvert.DeserializeObject<ProductCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductDel:
                    data = JsonConvert.DeserializeObject<ProductDelEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductDelEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductEdit:
                    data = JsonConvert.DeserializeObject<ProductEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductImageCreated:
                    data = JsonConvert.DeserializeObject<ProductImageCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductImageCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductImageEdit:
                    data = JsonConvert.DeserializeObject<ProductImageEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductImageEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductSkuDBCreate:
                    data = JsonConvert.DeserializeObject<ProductSkuDBCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductSkuDBCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductSkuDBUpdate:
                    data = JsonConvert.DeserializeObject<ProductSkuDBUpdateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductSkuDBUpdateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductSkuEdit:
                    data = JsonConvert.DeserializeObject<ProductSkuEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductSkuEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ProductSkuOrderNum:
                    data = JsonConvert.DeserializeObject<ProductSkuOrderNumEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ProductSkuOrderNumEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.RedoProductSku:
                    data = JsonConvert.DeserializeObject<RedoProductSkuEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<RedoProductSkuEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ResidueSkuUpdate:
                    data = JsonConvert.DeserializeObject<ResidueSkuUpdateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ResidueSkuUpdateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SaleStatusEdit:
                    data = JsonConvert.DeserializeObject<SaleStatusEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SaleStatusEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SellerStatistics:
                    data = JsonConvert.DeserializeObject<SellerStatisticsEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SellerStatisticsEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SellerStatisticsSumOrder:
                    data = JsonConvert.DeserializeObject<SellerStatisticsSumOrderEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SellerStatisticsSumOrderEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SellerStatisticsTrade:
                    data = JsonConvert.DeserializeObject<SellerStatisticsTradeEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SellerStatisticsTradeEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ShipOrderCreated:
                    data = JsonConvert.DeserializeObject<ShipOrderCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ShipOrderCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.ShipOrderEdit:
                    data = JsonConvert.DeserializeObject<ShipOrderEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<ShipOrderEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SysStatisticsCreate:
                    data = JsonConvert.DeserializeObject<SysStatisticsCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SysStatisticsCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SysStatisticsSumBuyMember:
                    data = JsonConvert.DeserializeObject<SysStatisticsSumBuyMemberEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SysStatisticsSumBuyMemberEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SysStatisticsSumNewMember:
                    data = JsonConvert.DeserializeObject<SysStatisticsSumNewMemberEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SysStatisticsSumNewMemberEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SysStatisticsSumOrder:
                    data = JsonConvert.DeserializeObject<SysStatisticsSumOrderEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SysStatisticsSumOrderEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.SysStatisticsSumUser:
                    data = JsonConvert.DeserializeObject<SysStatisticsSumUserEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<SysStatisticsSumUserEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.TokenCreated:
                    data = JsonConvert.DeserializeObject<TokenCreatedEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<TokenCreatedEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.TokenDisabled:
                    data = JsonConvert.DeserializeObject<TokenDisabledEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<TokenDisabledEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.TradeCreate:
                    data = JsonConvert.DeserializeObject<TradeCreateEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<TradeCreateEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.UpdateShoppingCartOrderID:
                    data = JsonConvert.DeserializeObject<UpdateShoppingCartOrderIDEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<UpdateShoppingCartOrderIDEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;
                case EventType.UseAmountEdit:
                    data = JsonConvert.DeserializeObject<UseAmountEditEvent>(text);
                    handlers = _eventHandlerFactory.GetHandlers<UseAmountEditEvent>() as IEnumerable<IEventHandler<Event>>;
                    break;

            }
            if (handlers != null && data != null)
            {
                foreach (var eventHandler in handlers)
                {
                    eventHandler.Handle(data);
                }
            }
            else
            {
                throw new UnregisteredDomainCommandException($"no handler registered or unknown event: [{text}]");
            }
        }
    }
}
