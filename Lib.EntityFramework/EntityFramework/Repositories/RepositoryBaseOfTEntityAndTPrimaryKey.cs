using LibMain.Domain.Entities;
using LibMain.Domain.Repositories;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lib.EntityFramework.EntityFramework.Repositories
{
    /// <summary>
    /// Implements IRepository for Entity Framework.
    /// </summary>
    /// <typeparam name="TDbContext">DbContext which contains <see cref="TEntity"/>.</typeparam>
    /// <typeparam name="TEntity">Type of the Entity for this repository</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key of the entity</typeparam>
    public class ServiceStackRepositoryBase<TDbContext, TEntity, TPrimaryKey> : LibRepositoryBase<TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
        where TDbContext : LibDbContext
    {
        /// <summary>
        /// Gets EF DbContext object.
        /// </summary>
        protected virtual TDbContext Context { get { return _dbContextProvider.DbContext; } }

        

        private readonly IDbContextProvider<TDbContext> _dbContextProvider;

        static ServiceStackRepositoryBase()
        {
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dbContextProvider"></param>
        public ServiceStackRepositoryBase(IDbContextProvider<TDbContext> dbContextProvider)
        {
            _dbContextProvider = dbContextProvider;
        }

        #region Select封装

        /// <summary>
        /// 通过lambda表达式返回数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public List<TEntity> Select(Expression<Func<TEntity, bool>> predicate, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync(predicate).Result
                        : db.Select(predicate);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式返回数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public List<TEntity> Select(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.Select(expression)
                        : db.Select(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }



        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public List<Into> Select<Into, From>(SqlExpression<From> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync<Into, From>(expression).Result
                        : db.Select<Into, From>(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public List<Into> Select<Into, From>(Func<SqlExpression<From>, SqlExpression<From>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync<Into, From>(expression).Result
                        : db.Select<Into, From>(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<TEntity> SelectByIds(IEnumerable idValues, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.SelectByIdsAsync<TEntity>(idValues).Result
                        : db.SelectByIds<TEntity>(idValues);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Single封装

        public override TEntity Single(Expression<Func<TEntity, bool>> predicate)
        {
            return this.Single(predicate);
        }
        /// <summary>
        /// 通过lambda表达式返回单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public TEntity Single(Expression<Func<TEntity, bool>> predicate, bool async = false)
        {
            using (var db = Context.OpenDbConnection())
            {
                return async
                    ? db.SingleAsync(predicate).Result
                    : db.Single(predicate);
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式返回单条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public TEntity Single(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false)
        {
            using (var db = Context.OpenDbConnection())
            {
                return async
                    ? db.SingleAsync(expression).Result
                    : db.Single(expression);
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式返回单条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public TEntity Single(SqlExpression<TEntity> expression, bool async = false)
        {
            using (var db = Context.OpenDbConnection())
            {
                return async
                    ? db.SingleAsync(expression).Result
                    : db.Single(expression);
            }
        }

        public TEntity SingleById(object idValue, bool async = false)
        {
            using (var db = Context.OpenDbConnection())
            {
                return async
                    ? db.SingleByIdAsync<TEntity>(idValue).Result
                    : db.SingleById<TEntity>(idValue);
            }
        }

        public TEntity SingleWhere(string name, object value, bool async = false)
        {
            using (var db = Context.OpenDbConnection())
            {
                return async
                    ? db.SingleWhereAsync<TEntity>(name, value).Result
                    : db.SingleWhere<TEntity>(name, value);
            }
        }

        #endregion

        #region Scalar封装
        

        /// <summary>
        /// 返回标量结果
        /// </summary>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long LongScalar(bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.LongScalarAsync().Result
                        : db.LongScalar();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Insert封装

        public long Insert(TEntity entity, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.InsertAsync(entity).Result
                        : db.Insert(entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void Insert(IEnumerable<TEntity> entities, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    if (async)
                    {
                        foreach (var entity in entities)
                        {
                            db.InsertAsync(entity);
                        }
                    }
                    else
                    {
                        foreach (var entity in entities)
                        {
                            db.Insert(entity);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Update封装

        /// <summary>
        /// 更新所有的字段，未赋值字段则为默认值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        public int Update(TEntity entity, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.UpdateAsync(entity).Result
                        : db.Update(entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }




        /// <summary>
        /// 更新所提供的字段，对象的int，bool必须设置为可空，否则设置int=0会失败
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        public override int UpdateNonDefaults(TEntity entity, Expression<Func<TEntity, bool>> where, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.UpdateNonDefaultsAsync(entity, where).GetAwaiter().GetResult()
                        : db.UpdateNonDefaults(entity, where);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public  int Update(object updateOnly, Expression<Func<TEntity, bool>> where = null, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.UpdateAsync(updateOnly, where).Result
                        : db.Update(updateOnly, where);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion

        #region Delete封装

        public int Delete(TEntity entity, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.DeleteAsync(entity).Result
                        : db.Delete(entity);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int Delete(Expression<Func<TEntity, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.DeleteAsync(expression).Result
                        : db.Delete(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int DeleteById(object id, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.DeleteByIdAsync<TEntity>(id).Result
                        : db.DeleteById<TEntity>(id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int DeleteByIds(IEnumerable idValues, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.DeleteByIdsAsync<TEntity>(idValues).Result
                        : db.DeleteByIds<TEntity>(idValues);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

        #region sql支持

        public int ExecuteSql(string sql, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExecuteSqlAsync(sql).Result
                        : db.ExecuteSql(sql);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public int ExecuteNonQuery(string sql, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExecuteNonQueryAsync(sql).Result
                        : db.ExecuteNonQuery(sql);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public void ExcuteProcedure(TEntity entity, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    if (async)
                    {
                        db.ExecuteProcedureAsync(entity);
                    }
                    else
                    {
                        db.ExecuteProcedure(entity);
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 执行存储过程包含一个返回值
        /// </summary>
        /// <param name="sql">sp的名称</param>
        /// <param name="inParams">参数</param>
        /// <param name="outputParam">返回的参数</param>
        /// <returns></returns>
        public object ExecuteProcedureWithOutput(string sqlName, object inParams, string outParam)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    var cmd = db.SqlProc(sqlName, inParams);
                    var result = cmd.AddParam(outParam, direction: ParameterDirection.Output);
                    cmd.ExecuteNonQuery();
                    return result.Value;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 执行存储过程
        /// </summary>
        /// <param name="sqlName"></param>
        /// <param name="inParams"></param>
        /// <returns></returns>
        public int ExecuteProcedureNonQuery(string sqlName, object inParams)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    var cmd = db.SqlProc(sqlName, inParams);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion

        #region 功能函数
        public override int Count()
        {
            return (int)this.Count();
        }
        /// <summary>
        /// 获取行计数
        /// </summary>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long Count(bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.CountAsync<TEntity>().Result
                        : db.Count<TEntity>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过lambda表达式，获取行计数
        /// </summary>
        /// <param name="expression">默认异步</param>
        /// <param name="async"></param>
        /// <returns></returns>
        public long Count(Expression<Func<TEntity, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.CountAsync(expression).Result
                        : db.Count(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式，获取行计数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long Count(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.CountAsync(expression).Result
                        : db.Count(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式，获取行计数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long Count(SqlExpression<TEntity> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.CountAsync(expression).Result
                        : db.Count(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式返回行数
        /// </summary>
        /// <param name="expression">表达式</param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long RowCount(SqlExpression<TEntity> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.RowCountAsync(expression).Result
                        : db.RowCount(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// 通过sql语句返回行数
        /// </summary>
        /// <param name="sql">sql语句</param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        public long RowCount(string sql, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.RowCountAsync(sql).Result
                        : db.RowCount(sql);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(Expression<Func<TEntity, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync(expression).Result
                        : db.Exists(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool Exists(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync(expression).Result
                        : db.Exists(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(SqlExpression<TEntity> expression, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync(expression).Result
                        : db.Exists(expression);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(object anonType, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync<TEntity>(anonType).Result
                        : db.Exists<TEntity>(anonType);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public bool Exists(string sql, object anonType = null, bool async = false)
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync<TEntity>(sql, anonType).Result
                        : db.Exists<TEntity>(sql, anonType);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        #region 写入日志
        private string logPath = @"D:\md\MHMD.Web\SQLOrm_LOG.txt";//MD.Framework.ConfigHelper.MonogErrorLog;
        private void WriteLog(string methodName, string exMsg)
        {
            //MD.Framework.Common.WriteFile(logPath, string.Format("Error:{0}\nMethod:{1} {2}", DateTime.Now, methodName, exMsg));
        }

        public override List<TEntity> GetAll()
        {
            try
            {
                using (var db = Context.OpenDbConnection())
                {
                    return db.Select<TEntity>();
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public override TEntity Insert(TEntity entity)
        {
            this.Insert(entity);
            return entity;
        }

        public override TEntity Update(TEntity entity)
        {
            this.Update(entity);
            return entity;
        }

        public override void Delete(TEntity entity)
        {
            this.Delete(entity);
        }

        public override void Delete(TPrimaryKey id)
        {
            throw new NotImplementedException();
        }

        #endregion
        #endregion
    }
}
