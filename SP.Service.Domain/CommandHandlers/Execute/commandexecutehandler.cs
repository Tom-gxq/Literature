using Grpc.Service.Core.Dependency;
using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.HandlerFactory;
using Grpc.Service.Core.Domain.Handlers;
using Newtonsoft.Json;
using SP.MongoDB.Repositories;
using SP.Service.Domain.Commands;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.Commands.BalancePay;
using SP.Service.Domain.Commands.Order;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.Commands.ShoppingCart;
using SP.Service.Domain.Commands.Statistics;
using SP.Service.Domain.Commands.StockShip;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.Exceptions;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers.Execute
{
    public class CommandExecuteHandler : ICommandExecuteHandler
    {
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly CommandReportDatabase _reportDatabase;
        public CommandExecuteHandler(ICommandHandlerFactory commandHandlerFactory, CommandReportDatabase reportDatabase)
        {
            _commandHandlerFactory = commandHandlerFactory;
            _reportDatabase = reportDatabase;
        }
        public void ExecuteCommand(string text)
        {
            var command = JsonConvert.DeserializeObject<SPCommand>(text);
            switch (command.CommandType)
            {
                case CommandType.BindOtherAccount:
                    ExecuteCommand<BindOtherAccountCommand>(text);
                    break;
                case CommandType.CreatAccount:
                    ExecuteCommand<CreatAccountCommand>(text);
                    break;
                case CommandType.CreatAddress:
                    ExecuteCommand<CreatAddressCommand>(text);
                    break;
                case CommandType.CreatAssociator:
                    ExecuteCommand<CreatAssociatorCommand>(text);
                    break;
                case CommandType.CreatAuthentication:
                    ExecuteCommand<CreatAuthenticationCommand>(text);
                    break;
                case CommandType.CreateAccessToken:
                    ExecuteCommand<CreateAccessTokenCommand>(text);
                    break;
                case CommandType.CreateAccountIDCard:
                    ExecuteCommand<CreateAccountIDCardCommand>(text);
                    break;
                case CommandType.CreateAccountPayPwd:
                    ExecuteCommand<CreateAccountPayPwdCommand>(text);
                    break;
                case CommandType.CreateApplyPartner:
                    ExecuteCommand<CreateApplyPartnerCommand>(text);
                    break;
                case CommandType.CreateCashApply:
                    ExecuteCommand<CreateCashApplyCommand>(text);
                    break;
                case CommandType.CreateOrder:
                    ExecuteCommand<CreateOrderCommand>(text);
                    break;
                case CommandType.CreateOtherAccount:
                    ExecuteCommand<CreateOtherAccountCommand>(text);
                    break;
                case CommandType.CreateProduct:
                    ExecuteCommand<CreateProductCommand>(text);
                    break;
                case CommandType.CreateProductSkuDB:
                    ExecuteCommand<CreateProductSkuDBCommand>(text);
                    break;
                case CommandType.CreatePurchaseOrder:
                    ExecuteCommand<CreatePurchaseOrderCommand>(text);
                    break;
                case CommandType.CreatShipOrder:
                    ExecuteCommand<CreatShipOrderCommand>(text);
                    break;
                case CommandType.CreatShoppingCart:
                    ExecuteCommand<CreatShoppingCartCommand>(text);
                    break;
                case CommandType.DelAccessToken:
                    ExecuteCommand<DelAccessTokenCommand>(text);
                    break;
                case CommandType.DelAddress:
                    ExecuteCommand<DelAddressCommand>(text);
                    break;
                case CommandType.DelProduct:
                    ExecuteCommand<DelProductCommand>(text);
                    break;
                case CommandType.EditAccount:
                    ExecuteCommand<EditAccountCommand>(text);
                    break;
                case CommandType.EditAccountInfo:
                    ExecuteCommand<EditAccountInfoCommand>(text);
                    break;
                case CommandType.EditAccountMobile:
                    ExecuteCommand<EditAccountMobileCommand>(text);
                    break;
                case CommandType.EditAccountPayPwd:
                    ExecuteCommand<EditAccountPayPwdCommand>(text);
                    break;
                case CommandType.EditAccountPwd:
                    ExecuteCommand<EditAccountPwdCommand>(text);
                    break;
                case CommandType.EditAddress:
                    ExecuteCommand<EditAddressCommand>(text);
                    break;
                case CommandType.EditAssociator:
                    ExecuteCommand<EditAssociatorCommand>(text);
                    break;
                case CommandType.EditAuthentication:
                    ExecuteCommand<EditAuthenticationCommand>(text);
                    break;
                case CommandType.EditOrder:
                    ExecuteCommand<EditOrderCommand>(text);
                    break;
                case CommandType.EditOrderCode:
                    ExecuteCommand<EditOrderCodeCommand>(text);
                    break;
                case CommandType.EditProduct:
                    ExecuteCommand<EditProductCommand>(text);
                    break;
                case CommandType.EditProductSku:
                    ExecuteCommand<EditProductSkuCommand>(text);
                    break;
                case CommandType.EditProductSkuDB:
                    ExecuteCommand<EditProductSkuDBCommand>(text);
                    break;
                case CommandType.EditPurchaseOrder:
                    ExecuteCommand<EditPurchaseOrderCommand>(text);
                    break;
                case CommandType.EditResidueSku:
                    ExecuteCommand<EditResidueSkuCommand>(text);
                    break;
                case CommandType.EditSaleStatus:
                    ExecuteCommand<EditSaleStatusCommand>(text);
                    break;
                case CommandType.EditShipOrderStatus:
                    ExecuteCommand<EditShipOrderStatusCommand>(text);
                    break;
                case CommandType.Generate:
                    ExecuteCommand<GenerateCommand>(text);                    
                    break;
                case CommandType.OrderDelStock:
                    ExecuteCommand<OrderDelStockCommand>(text);
                    break;
                case CommandType.OrderRedoStock:
                    ExecuteCommand<OrderRedoStockCommand>(text);
                    break;
                case CommandType.SumMemberStatistics:
                    ExecuteCommand<SumMemberStatisticsCommand>(text);
                    break;
                case CommandType.SumOrderStatistics:
                    ExecuteCommand<SumOrderStatisticsCommand>(text);
                    break;
                case CommandType.SumSellerStatistics:
                    ExecuteCommand<SumSellerStatisticsCommand>(text);
                    break;
                case CommandType.SumUserStatistics:
                    ExecuteCommand<SumUserStatisticsCommand>(text);
                    break;
                case CommandType.UpdateStatus:
                    ExecuteCommand<UpdateStatusCommand>(text);
                    break;
                case CommandType.BalancePay:
                    ExecuteCommand<BalancePayCommand>(text);
                    break;
                case CommandType.CreateSuppliersProduct:
                    ExecuteCommand<CreateSuppliersProductCommand>(text);
                    break;
                case CommandType.EditWxUnionId:
                    ExecuteCommand<EditWxUnionIdCommand>(text);
                    break;
                case CommandType.CreateWxOpenId:
                    ExecuteCommand<CreateWxOpenIdCommand>(text);
                    break;
                case CommandType.CreateSuppliersRegion:
                    ExecuteCommand<CreateSuppliersRegionCommand>(text);
                    break;
                case CommandType.CreateSellerProduct:
                    ExecuteCommand<CreateSellerProductCommand>(text);
                    break;
                case CommandType.DelSellerProduct:
                    ExecuteCommand<DelSellerProductCommand>(text);
                    break;
                default:
                    throw new UnregisteredDomainCommandException($" unknown command: [{text}]");
            }
            
        }
        private void ExecuteCommand<T>(string text) where T : SPCommand
        {
            var command = JsonConvert.DeserializeObject<T>(text);
            var tokenCmd = this._reportDatabase.GetByToken(command.Token);
            if (tokenCmd == null)
            {
                SaveCommand<T>(command);
                var commandHandler = _commandHandlerFactory.GetHandler<T>();
                if (commandHandler != null && command != null)
                {
                    commandHandler.Execute(command);
                    UpdateEventStatus<T>(command.Id, 1);
                }
                else
                {
                    throw new UnregisteredDomainCommandException($"no handler registered or unknown command: [{text}]");
                }
            }
            else
            {
                System.Console.WriteLine("tokenCmd:" + tokenCmd.GetMessage());
            }
        }
        private async void SaveCommand<T>(T command) where T: SPCommand
        {
            command.CommandId = command.Id.ToString();
            await this._reportDatabase.InsertAsync(command);
        }
        private async void UpdateEventStatus<T>(Guid commandId, int status) where T : SPCommand
        {
            await this._reportDatabase.UpdateCommandExcuteStatusAsync(commandId, status);
        }
    }
}
