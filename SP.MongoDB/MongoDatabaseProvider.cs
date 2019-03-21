using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Driver;
using Microsoft.Extensions.Configuration;
using Grpc.Service.Core;
using Grpc.Service.Core.Domain.Repositories;

namespace SP.MongoDB
{
    public class MongoDatabaseProvider : IMongoDatabaseProvider, ITransientDependency
    {
        private MongoClient client = null;
        private string DatatabaseName;
        public IMongoDatabase Database
        {
            get
            {
                return client.GetDatabase(this.DatatabaseName);
            }
        }

        public MongoDatabaseProvider(IConfigurationRoot config)
        {
            var url = config.GetSection("SP.MongoDb").Value;
            this.DatatabaseName = config.GetSection("SP.MongoDb.DatatabaseName").Value;
            this.client = new MongoClient(url);
        }
    }
}
