using AutoMapper;
using Grpc.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using RepeatedToken.Service.ReportCommand;
using SP.Service;
using SP.Service.Domain.Commands.Token;
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

        public override Task<RepeatedTokenResponse> GetRepeatedToken(RepeatedTokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Key} {AccountId}", request.Key,request.AccountId);
            RepeatedTokenResponse response = null;
            try
            {
                var input = Mapper.Map<ReadTokenCommand>(request);
                var output = RepeatedToken.getInstance().GetRepeatedToken(input); ;
                response = Mapper.Map<RepeatedTokenResponse>(output);
            }
            catch (Exception ex)
            {
                response = new RepeatedTokenResponse();
                response.Status = 10003;
                logger.LogError(this.prjLicEID, ex, "GetRepeatedToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GetRepeatedToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<RepeatedTokenResponse> GenerateRepeatedToken(AccountIdRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "GenerateRepeatedToken {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "GenerateRepeatedToken {AccountId}", request.AccountId);

            RepeatedTokenResponse response = null;
            try
            {
                var input = Mapper.Map<GenerateCommand>(request);
                input.AccessToken = System.Guid.NewGuid().ToString().Replace("-", string.Empty).ToLower();
                input.CreateTime = DateTime.Now;
                input.Status = true;
                var output = RepeatedToken.getInstance().GenerateRepeatedToken(input);
                response = Mapper.Map<RepeatedTokenResponse>(output);
                //response.RepeatedToken = Mapper.Map<SP.Service.RepeatedToken>(output.RepeatedToken);
            }
            catch (Exception ex)
            {
                response = new RepeatedTokenResponse();
                response.Status = 10003;
                logger.LogError(this.prjLicEID, ex, "GenerateRepeatedToken Exception");
            }
            logger.LogInformation(this.prjLicEID, "GenerateRepeatedToken {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }

        public override Task<RepeatedTokenResultResponse> UpdateRepeatedTokenDisabled(RepeatedTokenKeyRequest request, ServerCallContext context)
        {
            logger.LogInformation(this.prjLicEID, "UpdateRepeatedTokenDisabled {Date} {IPAdress} {Status} Connected! ", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), context.Peer, context.Status.ToString());
            logger.LogInformation(this.prjLicEID, "UpdateRepeatedTokenDisabled {Key} {AccountId}", request.Key, request.AccountId);
            RepeatedTokenResultResponse response = new RepeatedTokenResultResponse();
            response.Status = 10002;
            try
            {
                var input = Mapper.Map<UpdateStatusCommand>(request);
                input.Status = false;
                input.UpdateTime = DateTime.Now;
                var output = RepeatedToken.getInstance().UpdateRepeatedTokenDisabled(input);
                if(output)
                {
                    response.Status = 10001;
                }
            }
            catch (Exception ex)
            {
                response.Status = 10003;
                logger.LogError(this.prjLicEID, ex, "UpdateRepeatedTokenDisabled Exception");
            }
            logger.LogInformation(this.prjLicEID, "UpdateRepeatedTokenDisabled {Date} ReturnResult:{Result}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:ffff"), response.ToString());
            return Task.FromResult(response);
        }
    }
}
