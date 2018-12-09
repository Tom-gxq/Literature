using AutoMapper;
using SP.Service.Domain.Commands.Account;
using SP.Service.Domain.Commands.BalancePay;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.AutoMap
{
    public class AccountProfile: Profile
    {
        public override string ProfileName => "DomainAccountMappings";
        public AccountProfile()
        {
            // 输入
            CreateMap<CreatAccountCommand, AccountDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAccountCommand, RepeatedTokenDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateAccessTokenCommand, AccessTokenDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<DelAccessTokenCommand, AccessTokenDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreatAuthenticationCommand, AuthenticationDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAuthenticationCommand, AuthenticationDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreatAssociatorCommand, AssociatorDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAssociatorCommand, AssociatorDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateAccountPayPwdCommand, AccountInfoDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAccountPayPwdCommand, AccountInfoDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAccountPwdCommand, AccountDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditAccountMobileCommand, AccountDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<BindOtherAccountCommand, AccountDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateOtherAccountCommand, AccountDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateAccountIDCardCommand, AccountInfoDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateApplyPartnerCommand, ApplyPartnerDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreatAddressCommand, AccountAddressDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());            
            // 输入
            CreateMap<BalancePayCommand, BalancePayDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
        }
    }
}
