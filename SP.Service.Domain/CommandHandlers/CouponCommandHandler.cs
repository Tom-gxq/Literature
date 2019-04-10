using Grpc.Service.Core.AutoMapper;
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
    public class CouponCommandHandler : ICommandHandler<CreateCouponCommand>, ICommandHandler<PayCouponCommand>, ICommandHandler<UseCouponCommand>
    {
        private IDataRepository<CouponsDomain> _repository;
        private SysKindReportDatabase _kindReportDatabase;
        public CouponCommandHandler(IDataRepository<CouponsDomain> repository, SysKindReportDatabase kindReportDatabase)
        {
            this._repository = repository;
            this._kindReportDatabase = kindReportDatabase;
        }

        public void Execute(CreateCouponCommand command)
        {
            var kindDomain = _kindReportDatabase.GetSysKindById(command.KindId);
           
            var startDate = DateTime.Now;
            DateTime endDate = startDate;
            switch (kindDomain.Unit)
            {
                case 1://day
                    endDate = startDate.AddDays(kindDomain.Quantity);
                    break;
                case 2://month
                    endDate = startDate.AddMonths(kindDomain.Quantity);
                    break;
                case 3://year
                    endDate = startDate.AddYears(kindDomain.Quantity);
                    break;
            }

            var aggregate = command.ToDomain<CouponsDomain>();
            aggregate.StartDate = startDate;
            aggregate.EndDate = endDate;
            aggregate.Create(kindDomain.Num);            
            _repository.Save(aggregate);
        }

        public void Execute(PayCouponCommand command)
        {
            var aggregate = command.ToDomain<CouponsDomain>();
            aggregate.Payed();
            _repository.Save(aggregate);
        }

        public void Execute(UseCouponCommand command)
        {
            var aggregate = command.ToDomain<CouponsDomain>();
            aggregate.Used();
            _repository.Save(aggregate);
        }
    }
}
