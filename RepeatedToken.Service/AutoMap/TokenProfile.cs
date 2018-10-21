using AutoMapper;
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
            CreateMap<SendMessageRequest, SendInput>();

            // 输出
            CreateMap<SendOutput, SendMessageResponse>();
            // 输入
            CreateMap<HttpRequest, HttpInput>();

            // 输出
            CreateMap<HttpOutput, HttpResponse>();
        }
    }
}
