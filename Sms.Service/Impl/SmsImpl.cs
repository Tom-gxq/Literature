using AutoMapper;
using Grpc.Core;
using MD.SmsService;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Sms.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static MD.SmsService.Sms;

namespace Sms.Service.Impl
{
    public class SmsImpl : SmsBase
    {
        private ILogger logger = new ServiceCollection()
              .AddLogging()
              .BuildServiceProvider()
              .GetService<ILoggerFactory>()
              .AddConsole()
              .CreateLogger("SmsService");

        public override Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
        {
            logger.LogInformation(7800, "{Date} {IPAdress} {Status} SendMessage Connected! AccountId:[{AccountId}] Mobile:[{Mobile}]",
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.FromAccountId, request.Mobile);
            var input = Mapper.Map<SendInput>(request);
            SendMessageResponse response = null; 
            try
            { 
                var output = new Sender(input).Send();
                response = Mapper.Map<SendMessageResponse>(output);

            }
            catch (Exception ex)
            {
                logger.LogError(7800, ex, "SendMessage Exception");
            }
        
            logger.LogInformation(7800, "SendMessage {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
        public override Task<SendMessageResponse> CheckIsAllowSendRegisterMobileMessage(RegisterRequest request, ServerCallContext context)
        {
            logger.LogInformation(7800, "{Date} {IPAdress} {Status} CheckIsAllowSendRegisterMobileMessage Connected! Ip:[{Ip}] MobilePhone:[{MobilePhone}]",
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Ip, request.MobilePhone);
            var mobilePhone = request.MobilePhone.Replace("+", string.Empty);

            var sendLimit = new SendLimit.Limits.Register()
            {
                MobilePhone = mobilePhone,
                IP = request.Ip
            };
            var response = new SendMessageResponse();
            try
            {
                var isAllow = sendLimit.IsAllowSend();

                if (isAllow)
                {
                    response.Code = 0;
                }
                else
                {
                    response.Code = 1;
                }
            }
            catch (Exception ex)
            {
                logger.LogError(7800, ex, "CheckIsAllowSendRegisterMobileMessage Exception");
            }
            logger.LogInformation(7800, "CheckIsAllowSendRegisterMobileMessage {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response); ;
        }

        public override Task<SendMessageResponse> SetRegisterMobileMessageLimit(RegisterRequest request, ServerCallContext context)
        {
            logger.LogInformation(7800, "{Date} {IPAdress} {Status} SetRegisterMobileMessageLimit Connected! Ip:[{Ip}] MobilePhone:[{MobilePhone}]",
               DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString(), request.Ip, request.MobilePhone);
            var mobilePhone = request.MobilePhone.Replace("+", string.Empty);

            var sendLimit = new SendLimit.Limits.Register()
            {
                MobilePhone = mobilePhone,
                IP = request.Ip
            };
            var response = new SendMessageResponse();
            try
            {
                sendLimit.SetLimitNumber();            
                response.Code = 0;
            }
            catch (Exception ex)
            {
                logger.LogError(7800, ex, "SetRegisterMobileMessageLimit Exception");
            }
            logger.LogInformation(7800, "SetRegisterMobileMessageLimit {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

    }
}
