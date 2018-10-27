using RepeatedToken.Service.ReportCommand;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.AutoMapper;
using SP.Service.Entity;
using SP.Service.Domain.Commands.Token;

namespace RepeatedToken.Service
{
    internal class RepeatedToken
    {
        private static RepeatedToken instance = new RepeatedToken();
        private  RepeatedToken()
        {

        }
        public static RepeatedToken getInstance()
        {
            return instance;
        }

        public TokenModel GenerateRepeatedToken(GenerateCommand command)
        {            
            ServiceLocator.CommandBus.Send(command);
            var model = new TokenModel();
            model.Status = 10001;
            model.RepeatedToken = command.ToModel<RepeatedTokenModel>();
            return model;
        }
        public TokenModel GetRepeatedToken(ReadTokenCommand command)
        {
            var domain = ServiceLocator.ReportDatabase.GetTokenByKey(command.AccountId, command.Key);
            var model = new TokenModel();
            if (domain != null)
            {
                model.RepeatedToken = domain.ToModel<RepeatedTokenModel>();
                model.Status = 10001;
            }
            else
            {
                model.Status = 10002;
            }
            return model;
        }

        public bool UpdateRepeatedTokenDisabled(UpdateStatusCommand command)
        {
            ServiceLocator.CommandBus.Send(command);
            
            return true;
        }
    }
}
