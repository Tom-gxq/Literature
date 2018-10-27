using Grpc.Service.Core.Domain.Handlers;
using SP.Service.Domain.Events;
using SP.Service.Domain.Reporting;
using SP.Service.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Grpc.Service.Core.AutoMapper;

namespace SP.Service.Domain.EventHandlers
{
    public class TokenEventHandler : IEventHandler<TokenCreatedEvent>,IEventHandler<TokenDisabledEvent>
    {
        private readonly RepeatedTokenReportDatabase _reportDatabase;
        public TokenEventHandler(RepeatedTokenReportDatabase reportDatabase)
        {
            _reportDatabase = reportDatabase;
        }
        public void Handle(TokenCreatedEvent handle)
        {
            var entity = handle.ToEntity<RepeatedTokenEntity>();

            _reportDatabase.Add(entity);
        }

        public void Handle(TokenDisabledEvent handle)
        {
            var entity = handle.ToEntity<RepeatedTokenEntity>();

            _reportDatabase.Update(entity);
        }
    }
}
