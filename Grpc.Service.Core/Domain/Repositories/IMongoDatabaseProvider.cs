using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Grpc.Service.Core.Domain.Repositories
{
    public interface IMongoDatabaseProvider
    {
        /// <summary>
        /// Gets the <see cref="MongoDatabase"/>.
        /// </summary>
        IMongoDatabase Database { get; }
    }
}
