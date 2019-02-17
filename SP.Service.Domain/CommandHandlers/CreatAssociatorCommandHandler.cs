using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Producer;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class CreatAssociatorCommandHandler : ICommandHandler<CreatAssociatorCommand>
    {
        private IDataRepository<AssociatorDomain> _repository;
        private IDataRepository<SysStatisticsDomain> _sysRepository;
        private SysKindReportDatabase _kindReportDatabase;
        private SysStatisticsReportDatabase _sysStatisticsReportDatabase;
        private AssociatorReportDatabase _reportDatabase;

        public CreatAssociatorCommandHandler(IDataRepository<AssociatorDomain> repository, IDataRepository<SysStatisticsDomain> sysRepository,
            SysKindReportDatabase kindReportDatabase, SysStatisticsReportDatabase sysStatisticsReportDatabase)
        {
            this._repository = repository;
            this._sysRepository = sysRepository;
            this._kindReportDatabase = kindReportDatabase;
            this._sysStatisticsReportDatabase = sysStatisticsReportDatabase;
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
            var list = _reportDatabase.GetAssociatorByAccountId(command.AccountId);
            var startDate = list?.Max(x=>x.EndDate)??DateTime.Now;
            var aggregate = new AssociatorDomain(command.Id, command.AccountId, command.KindId, 
                command.Quantity, payOrderCode, command.PayType, amount, startDate.AddDays(1));
            //会员购买统计
            aggregate.AddMemberKafkaInfo(command.Id,command.AccountId, amount, AuthorizeType.Buy);
            _repository.Save(aggregate);
        }
    }
}
