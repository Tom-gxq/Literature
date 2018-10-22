using AutoMapper;
using RepeatedToken.Service.ReportCommand;
using SP.Service;
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
            // 输入
            CreateMap<TokenKeyRequest, ReadTokenCommand>();

            // 输出
            CreateMap<SendOutput, SendMessageResponse>();
            // 输入
            CreateMap<HttpRequest, HttpInput>();

            // 输出
            CreateMap<HttpOutput, HttpResponse>();
        }
    }
}
