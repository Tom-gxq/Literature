using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SP.Service;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static SP.Service.RepeatedTokenService;

namespace RepeatedToken.Service.GrpcImpl
{
    internal class TokenServiceImpl: RepeatedTokenServiceBase
    {
        private ILogger logger = new ServiceCollection()
             .AddLogging()
             .BuildServiceProvider()
             .GetService<ILoggerFactory>()
             .AddConsole()
             .CreateLogger("RepeatedTokenService");

        private int prjLicEID = 17000;

        public TokenServiceImpl(int port)
        {
            if (port > 0)
            {
                this.prjLicEID = port;
            }
        }

        public override Task<RepeatedTokenResponse> GetRepeatedToken(TokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Key} {AccountId}", request.Key,request.AccountId);
            RepeatedTokenResponse response = null;
            try
            {
                var input = Mapper.Map<SendInput>(request);
                var output = TokenBusiness.GetRepeatedToken(request.AccountId, request.Key);
                response = Mapper.Map<SendMessageResponse>(output);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetRepeatedToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<RepeatedTokenResultResponse> AddRepeatedToken(RepeatedTokenRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "AddRepeatedToken {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "AddRepeatedToken {AccessToken} {AccountId} {CreateTime}", request.AccessToken.AccessToken, request.AccessToken.AccountId, new DateTime(request.AccessToken.CreateTime).ToLongDateString());
            RepeatedTokenResultResponse response = null;
            try
            {
                response = TokenBusiness.GetRepeatedToken(request.AccountId, request.Key);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "AddRepeatedToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "AddRepeatedToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<RepeatedTokenResultResponse> UpdateRepeatedTokenDisabled(TokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Key} {AccountId}", request.Key, request.AccountId);
            RepeatedTokenResultResponse response = null;
            try
            {
                response = TokenBusiness.GetRepeatedToken(request.AccountId, request.Key);
            }
            catch (Exception ex)
            {
                logger.LogError(this.prjLicEID, ex, "GetRepeatedToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
