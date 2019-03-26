using AutoMapper;
using SP.Service.Domain.Commands.Product;
using SP.Service.Domain.DomainEntity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.Service.Domain.AutoMap
{
    public class SellerProductProfile : Profile
    {
        public override string ProfileName => "DomainSellerProductMappings";
        public SellerProductProfile()
        {
            // 输入
            CreateMap<CreateSuppliersProductCommand, SuppliersProductDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateSellerProductCommand, SellerProductDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateSuppliersRegionCommand, AccessTokenDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<DelSellerProductCommand, AccessTokenDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<EditSaleStatusCommand, AuthenticationDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            // 输入
            CreateMap<CreateProductCommand, AuthenticationDomain>()
                .ForSourceMember(command => command.CommandType, domain => domain.Ignore())
                .ForSourceMember(command => command.ExcuteStatus, domain => domain.Ignore())
                .ForSourceMember(command => command.TopicTitle, domain => domain.Ignore());
            
        }
    }
}
