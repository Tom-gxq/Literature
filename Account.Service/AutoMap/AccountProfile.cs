using AutoMapper;
using SP.Service;
using SP.Service.Domain.DomainEntity;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Account.Service.AutoMap
{
    class AccountProfile : Profile
    {
        public override string ProfileName => "GrpcAccountMappings";
        public AccountProfile()
        {
            CreateMap<int, Guid>().ConvertUsing<GuidTypeConverter>();            
            // 输出
            CreateMap<TradeDomain, Trade > ()
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(domain => domain.CreateTime.Ticks))
                .ForMember(model => model.TradeNo, opt =>
                {
                    opt.NullSubstitute("");
                })
                .ForMember(model => model.Type, opt => opt.MapFrom(domain => domain.TradeType));
            // 输入
            CreateMap<CashApplyEntity, TradeDomain>()
                .ForMember(domain => domain.Id, domain => domain.Ignore());

            CreateMap<ComTradeEntity, TradeDomain>()
                .ForMember(domain => domain.Id, domain => domain.Ignore());

            CreateMap<ConsumeTradeEntity, TradeDomain>()
                .ForMember(domain => domain.Id, domain => domain.Ignore());
        }
    }
}
