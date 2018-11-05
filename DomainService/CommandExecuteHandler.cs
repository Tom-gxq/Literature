using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Newtonsoft.Json;
using SP.Service.Domain.Commands;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.Commands.ShoppingCart;
using SP.Service.Domain.Commands.Statistics;
using SP.Service.Domain.Commands.StockShip;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DomainService
{
    class CommandExecuteHandler : ICommandExecuteHandler
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        public CommandExecuteHandler(ICommandHandlerFactory commandHandlerFactory)
        {
            _commandHandlerFactory = commandHandlerFactory;
        }
        public void ExecuteCommand(string text)
        {
            var command = JsonConvert.DeserializeObject<Command>(text);
            Command data = null;
            ICommandHandler<Command> handler = null;
            switch (command.CommandType)
            {
                case CommandType.BindOtherAccount:
                    data = JsonConvert.DeserializeObject<BindOtherAccountCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<BindOtherAccountCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatAccount:
                    data = JsonConvert.DeserializeObject<CreatAccountCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatAccountCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatAddress:
                    data = JsonConvert.DeserializeObject<CreatAddressCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatAddressCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatAssociator:
                    data = JsonConvert.DeserializeObject<CreatAssociatorCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatAssociatorCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatAuthentication:
                    data = JsonConvert.DeserializeObject<CreatAuthenticationCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatAuthenticationCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateAccessToken:
                    data = JsonConvert.DeserializeObject<CreateAccessTokenCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateAccessTokenCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateAccountIDCard:
                    data = JsonConvert.DeserializeObject<CreateAccountIDCardCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateAccountIDCardCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateAccountPayPwd:
                    data = JsonConvert.DeserializeObject<CreateAccountPayPwdCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateAccountPayPwdCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateApplyPartner:
                    data = JsonConvert.DeserializeObject<CreateApplyPartnerCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateApplyPartnerCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateCashApply:
                    data = JsonConvert.DeserializeObject<CreateCashApplyCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateCashApplyCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateOrder:
                    data = JsonConvert.DeserializeObject<CreateOrderCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateOrderCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateOtherAccount:
                    data = JsonConvert.DeserializeObject<CreateOtherAccountCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateOtherAccountCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateProduct:
                    data = JsonConvert.DeserializeObject<CreateProductCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateProductCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreateProductSkuDB:
                    data = JsonConvert.DeserializeObject<CreateProductSkuDBCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreateProductSkuDBCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatePurchaseOrder:
                    data = JsonConvert.DeserializeObject<CreatePurchaseOrderCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatePurchaseOrderCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatShipOrder:
                    data = JsonConvert.DeserializeObject<CreatShipOrderCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatShipOrderCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.CreatShoppingCart:
                    data = JsonConvert.DeserializeObject<CreatShoppingCartCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<CreatShoppingCartCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.DelAccessToken:
                    data = JsonConvert.DeserializeObject<DelAccessTokenCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<DelAccessTokenCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.DelAddress:
                    data = JsonConvert.DeserializeObject<DelAddressCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<DelAddressCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.DelProduct:
                    data = JsonConvert.DeserializeObject<DelProductCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<DelProductCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAccount:
                    data = JsonConvert.DeserializeObject<EditAccountCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAccountCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAccountInfo:
                    data = JsonConvert.DeserializeObject<EditAccountInfoCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAccountInfoCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAccountMobile:
                    data = JsonConvert.DeserializeObject<EditAccountMobileCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAccountMobileCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAccountPayPwd:
                    data = JsonConvert.DeserializeObject<EditAccountPayPwdCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAccountPayPwdCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAccountPwd:
                    data = JsonConvert.DeserializeObject<EditAccountPwdCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAccountPwdCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAddress:
                    data = JsonConvert.DeserializeObject<EditAddressCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAddressCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAssociator:
                    data = JsonConvert.DeserializeObject<EditAssociatorCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAssociatorCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditAuthentication:
                    data = JsonConvert.DeserializeObject<EditAuthenticationCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditAuthenticationCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditOrder:
                    data = JsonConvert.DeserializeObject<EditOrderCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditOrderCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditOrderCode:
                    data = JsonConvert.DeserializeObject<EditOrderCodeCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditOrderCodeCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditProduct:
                    data = JsonConvert.DeserializeObject<EditProductCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditProductCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditProductSku:
                    data = JsonConvert.DeserializeObject<EditProductSkuCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditProductSkuCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditProductSkuDB:
                    data = JsonConvert.DeserializeObject<EditProductSkuDBCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditProductSkuDBCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditPurchaseOrder:
                    data = JsonConvert.DeserializeObject<EditPurchaseOrderCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditPurchaseOrderCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditResidueSku:
                    data = JsonConvert.DeserializeObject<EditResidueSkuCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditResidueSkuCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditSaleStatus:
                    data = JsonConvert.DeserializeObject<EditSaleStatusCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditSaleStatusCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.EditShipOrderStatus:
                    data = JsonConvert.DeserializeObject<EditShipOrderStatusCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<EditShipOrderStatusCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.Generate:
                    data = JsonConvert.DeserializeObject<GenerateCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<GenerateCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.OrderDelStock:
                    data = JsonConvert.DeserializeObject<OrderDelStockCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<OrderDelStockCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.OrderRedoStock:
                    data = JsonConvert.DeserializeObject<OrderRedoStockCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<OrderRedoStockCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.SumMemberStatistics:
                    data = JsonConvert.DeserializeObject<SumMemberStatisticsCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<SumMemberStatisticsCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.SumOrderStatistics:
                    data = JsonConvert.DeserializeObject<SumOrderStatisticsCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<SumOrderStatisticsCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.SumSellerStatistics:
                    data = JsonConvert.DeserializeObject<SumSellerStatisticsCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<SumSellerStatisticsCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.SumUserStatistics:
                    data = JsonConvert.DeserializeObject<SumUserStatisticsCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<SumUserStatisticsCommand>() as ICommandHandler<Command>;
                    break;
                case CommandType.UpdateStatus:
                    data = JsonConvert.DeserializeObject<UpdateStatusCommand>(text);
                    handler = _commandHandlerFactory.GetHandler<UpdateStatusCommand>() as ICommandHandler<Command>;
                    break;
            }
            if(handler != null && data != null)
            {
                handler.Execute(data);
            }
            else
            {
                throw new UnregisteredDomainCommandException($"no handler registered or unknown command: [{text}]");
            }
        }
    }
}
