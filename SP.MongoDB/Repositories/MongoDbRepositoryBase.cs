using Grpc.Service.Core.Domain.Entity;
using Grpc.Service.Core.Domain.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SP.MongoDB.Repositories
{
    public class MongoDbRepositoryBase<TEntity, TPrimaryKey> : IRepositoryBase<TEntity, TPrimaryKey>
        where TPrimaryKey : IEquatable<TPrimaryKey>        
    {
        private readonly IMongoDatabaseProvider _databaseProvider;

        protected IMongoDatabase Database
        {
            get { return _databaseProvider.Database; }
        }

        protected IMongoCollection<TEntity> Collection
        {
            get
            {
                return _databaseProvider.Database.GetCollection<TEntity>(typeof(TEntity).Name);
            }
        }

        public MongoDbRepositoryBase(IMongoDatabaseProvider databaseProvider)
        {
            _databaseProvider = databaseProvider;
        }

        public IMongoQueryable<TEntity> GetAll()
        {
            return Collection.AsQueryable();
        }

        public virtual TEntity Get(TPrimaryKey id)
        {
            //var query = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            //return Collection.Find(query).First(); //TODO: What if no entity with id?
            return default(TEntity);
        }

        public virtual TEntity FirstOrDefault(TPrimaryKey id)
        {
            return default(TEntity);
        }

        public virtual TEntity Insert(TEntity entity)
        {
            Collection.InsertOne(entity);
            return entity;
        }
        public virtual TEntity Update(TEntity entity)
        {
            //var filter = Builders<TEntity>.Filter.Eq(e => e.Id, entity.Id);
            //var update = Builders<TEntity>.Update.Set(a=>a.Id,entity.Id);
            //Collection.UpdateOne(filter, update);
            return entity;
        }

        public virtual void Delete(TEntity entity)
        {
            //Delete(entity.Id);
        }

        public virtual void Delete(TPrimaryKey id)
        {
            //var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
            //Collection.DeleteOne(filter);
        }
    }
}
