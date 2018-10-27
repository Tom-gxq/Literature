using AutoMapper;
using RepeatedToken.Service.ReportCommand;
using SP.Service;
using SP.Service.Domain.Commands.Token;
using SP.Service.Domain.DomainEntity;
using SP.Service.Domain.Events;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepeatedToken.Service.AutoMap
{
    class TokenProfile:Profile
    {
        public override string ProfileName => "GrpcTokenMappings";

        public TokenProfile()
        {
            // 输出
            CreateMap<RepeatedTokenModel, SP.Service.RepeatedToken>();
            CreateMap<TokenModel, RepeatedTokenResponse>();
            
            // 输出
            CreateMap<RepeatedTokenDomain, RepeatedTokenModel>()
            .ForMember(model => model.CreateTime, opt => opt.MapFrom(domain => domain.CreateTime.Ticks));

            // 输出
            CreateMap<GenerateCommand, RepeatedTokenModel>()
                .ForMember(model => model.CreateTime, opt => opt.MapFrom(domain => domain.CreateTime.Ticks));

            // 输入
            CreateMap<RepeatedTokenKeyRequest, ReadTokenCommand>();
            // 输入
            CreateMap<RepeatedTokenKeyRequest, UpdateStatusCommand>()
                .ForMember(command=> command.AccessToken ,opt=>opt.MapFrom(request=>request.Key));
            // 输入
            CreateMap<AccountIdRequest, GenerateCommand>();
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
