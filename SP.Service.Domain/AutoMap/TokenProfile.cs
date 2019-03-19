using AutoMapper;
using SP.Api.Model.RepeatedToken;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.AutoMap
{
    public class TokenProfile : Profile
    {
        public override string ProfileName => "DomainTokenMappings";

        public TokenProfile()
        {              
            // 输入
            CreateMap<GenerateCommand, RepeatedTokenDomain>();
            // 输入
            CreateMap<UpdateStatusCommand, RepeatedTokenDomain>();
            // 输入
            CreateMap<TokenCreatedEvent, RepeatedTokenEntity>();
            // 输入
            CreateMap<TokenDisabledEvent, RepeatedTokenEntity>();

        }
    }
}
