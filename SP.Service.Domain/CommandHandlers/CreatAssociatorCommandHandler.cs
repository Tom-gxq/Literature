using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatAssociatorCommandHandler : ICommandHandler<CreatAssociatorCommand>
    {
        private IDataRepository<AssociatorDomain> _repository;
        private SysKindReportDatabase _kindReportDatabase;

        public CreatAssociatorCommandHandler(IDataRepository<AssociatorDomain> repository, SysKindReportDatabase kindReportDatabase)
        {
            this._repository = repository;
            _kindReportDatabase = kindReportDatabase;
        }

        public void Execute(CreatAssociatorCommand command)
        {
            var kindDomain = _kindReportDatabase.GetSysKindById(command.KindId);
            double amount = 0;
            if(kindDomain != null)
            {
                amount = kindDomain.Price * command.Quantity;
            }
            var payOrderCode = "GMHY" + DateTime.Now.ToString("yyyyMMddHH24mmssffff");
            var aggregate = new AssociatorDomain(command.Id, command.AccountId, command.KindId, command.Quantity, payOrderCode, command.PayType, amount);

            _repository.Save(aggregate);
        }
    }
}
