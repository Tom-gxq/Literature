using Grpc.Service.Core.Domain.Commands;
using Grpc.Service.Core.Domain.Repositories;
using LanguageExt;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.MongoDB.Repositories
{
    public class CommandMongoDbRepository: MongoDbRepositoryBase<SPCommand, string>
    {
        public CommandMongoDbRepository(IMongoDatabaseProvider databaseProvider):base(databaseProvider)
        {
            
        }
        public override SPCommand Get(string commandId)
        {
            var query = Builders<SPCommand>.Filter.Eq(e => e.CommandId, commandId);
            return Collection.Find(query).First();             
        }

        public override SPCommand FirstOrDefault(string commandId)
        {
            var query = Builders<SPCommand>.Filter.Eq(e => e.CommandId, commandId);
            return Collection.Find(query).FirstOrDefault();
        }
        public SPCommand GetByToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                var query = Builders<SPCommand>.Filter.Eq(e => e.Token, token);
                var projection = Builders<SPCommand>.Projection
                    .Expression(x=>new SPCommand() { Token =x.Token, TopicTitle =x.TopicTitle,
                        CommandType =x.CommandType , ExcuteStatus =x.ExcuteStatus,
                        CommandId = x.CommandId} );
                var list = Collection.Find(query).Project(projection);
                if (list == null || list.CountDocuments() <= 0)
                {
                    return null;
                }
                else
                {
                    return list.First();
                }
            }
            else
            {
                return null;
            }
        }
        public bool UpdateCommandExcuteStatus(Guid commandId,int status)
        {
            var filter = Builders<SPCommand>.Filter.Eq(e => e.CommandId, commandId.ToString());
            var update = Builders<SPCommand>.Update.Set(a => a.ExcuteStatus, status);
            Collection.UpdateOne(filter, update);
            return true;
        }
    }
}
