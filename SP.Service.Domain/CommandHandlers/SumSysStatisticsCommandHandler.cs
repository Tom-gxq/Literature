using Grpc.Service.Core.Domain.Handlers;
using Grpc.Service.Core.Domain.Storage;
using SP.Service.Domain.Commands.Statistics;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Reporting;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.CommandHandlers
{
    public class SumSysStatisticsCommandHandler : ICommandHandler<SumUserStatisticsCommand>, 
        ICommandHandler<SumMemberStatisticsCommand>
    {
        private IDataRepository<SysStatisticsDomain> _sysRepository;
        private SysStatisticsReportDatabase _sysStatisticsReportDatabase;

        public SumSysStatisticsCommandHandler(IDataRepository<SysStatisticsDomain> sysRepository,
            SysStatisticsReportDatabase sysStatisticsReportDatabase)
        {
            this._sysRepository = sysRepository;
            this._sysStatisticsReportDatabase = sysStatisticsReportDatabase;
        }

        public void Execute(SumUserStatisticsCommand command)
        {
            SysStatisticsDomain sysStatisticsDomain = null;
            var sysEntity = _sysStatisticsReportDatabase.GetTodaySysStatistic(command.CreateTime);
            if (sysEntity != null && sysEntity.CreateTime > DateTime.MinValue)
            {
                sysStatisticsDomain = new SysStatisticsDomain();
                sysStatisticsDomain.SumUserStatistics(command.Id.ToString(), command.CreateTime);
            }
            else
            {
                sysStatisticsDomain = new SysStatisticsDomain(command.CreateTime, 1, 0, 0, 0, 0,0);
            }
            if (sysStatisticsDomain != null)
            {
                _sysRepository.Save(sysStatisticsDomain);
            }
        }
        public void Execute(SumMemberStatisticsCommand command)
        {
            SysStatisticsDomain memberDomain = null;
            var memberEntity = _sysStatisticsReportDatabase.GetTodaySysStatistic(command.CreateTime);
            if (command.Type == SP.Producer.AuthorizeType.Present)
            {
                if (memberEntity != null & memberEntity.CreateTime > DateTime.MinValue)
                {
                    memberDomain = new SysStatisticsDomain();
                    memberDomain.SumNewMemberStatistics(command.AccountId, command.Amount, command.CreateTime);
                }
                else
                {
                    memberDomain = new SysStatisticsDomain(command.CreateTime, 0, 1, 0, 0, 0,0);
                }
                
            }
            else if(command.Type == SP.Producer.AuthorizeType.Buy)
            {
                if (memberEntity != null && memberEntity.CreateTime > DateTime.MinValue)
                {
                    memberDomain = new SysStatisticsDomain();
                    memberDomain.SumBuyMemberStatistics(command.AccountId, command.Amount, command.CreateTime);
                }
                else
                {
                    memberDomain = new SysStatisticsDomain(command.CreateTime, 0, 1, 0, 0, 0,0);
                }
            }
            if (memberDomain != null)
            {
                _sysRepository.Save(memberDomain);
            }
        }
    }
}
