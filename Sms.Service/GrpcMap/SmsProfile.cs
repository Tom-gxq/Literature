using AutoMapper;
using MD.SmsService;
using Sms.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sms.Service.GrpcMap
{
    public class SmsProfile : Profile
    {
        public override string ProfileName => "GrpcSmsMappings";

        public SmsProfile()
        {
            // 输入
            CreateMap<SendMessageRequest, SendInput>();

            // 输出
            CreateMap<SendOutput, SendMessageResponse>();
        }
    }
}
