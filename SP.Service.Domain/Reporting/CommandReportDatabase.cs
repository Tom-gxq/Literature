using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.Reporting;
using SP.MongoDB.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SP.Service.Domain.Reporting
{
    public class CommandReportDatabase : IReportDatabase
    {
        private readonly CommandMongoDbRepository _repository;
        public CommandReportDatabase(CommandMongoDbRepository repository)
        {
            _repository = repository;
        }

        public SPCommand GetByToken(string token)
        {
            return _repository.GetByToken(token);
        }
        public bool UpdateCommandExcuteStatus(Guid commandId, int status)
        {
            return _repository.UpdateCommandExcuteStatus(commandId, status);
        }
        public async Task<bool> UpdateCommandExcuteStatusAsync(Guid commandId, int status)
        {
            return _repository.UpdateCommandExcuteStatus(commandId, status);
        }

        public SPCommand Insert(SPCommand entity)
        {
            return _repository.Insert(entity); ;
        }
        public async Task<SPCommand> InsertAsync(SPCommand entity)
        {
            return _repository.Insert(entity); ;
        }
    }
}
