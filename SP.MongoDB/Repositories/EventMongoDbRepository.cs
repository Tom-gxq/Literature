using Grpc.Service.Core.Domain.Events;
using Grpc.Service.Core.Domain.Repositories;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace SP.MongoDB.Repositories
{
    public class EventMongoDbRepository: MongoDbRepositoryBase<Event, string>
    {
        public EventMongoDbRepository(IMongoDatabaseProvider databaseProvider):base(databaseProvider)
        {

        }
        public override Event Get(string eventId)
        {
            var query = Builders<Event>.Filter.Eq(e => e.EventId, eventId);
            return Collection.Find(query).First();
        }

        public override Event FirstOrDefault(string eventId)
        {
            var query = Builders<Event>.Filter.Eq(e => e.EventId, eventId);
            return Collection.Find(query).FirstOrDefault();
        }
        
        public bool UpdateEventExcuteStatus(string eventId, int status)
        {
            var filter = Builders<Event>.Filter.Eq(e => e.EventId, eventId);
            var update = Builders<Event>.Update.Set(a => a.ExcuteStatus, status);
            Collection.UpdateOne(filter, update);
            return true;
        }
    }
}
