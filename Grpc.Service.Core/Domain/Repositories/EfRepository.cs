using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;
using Grpc.Service.Core.Domain.Entity;
using ServiceStack.OrmLite;
using ServiceStack.OrmLite.Legacy;

namespace Grpc.Service.Core.Domain.Repositories
{
    /// <summary>
    /// Entity Framework repository
    /// </summary>
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        #region Fields

        private readonly IDataContext _context;

        #endregion

        #region Ctor

        /// <summary>
        /// Ctor
        /// </summary>
        /// <param name="context">Object context</param>
        public EfRepository(IDataContext context)
        {
            this._context = context;
            OrmLiteConfig.DialectProvider = SqlServerDialect.Provider;
        }

        #endregion

        /// <summary>
        /// 数据库连接
        /// </summary>
        protected string DbConnection;
        protected IDbConnection OpenDbConnection()
        {
            if (DbConnection == null)
            {
                throw new Exception("Connection string \"DbConnection\" can not be null.");
            }

            return DbConnection.OpenDbConnection();
        }

        #region Select封装

        /// <summary>
        /// 通过lambda表达式返回数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected List<T> Select(Expression<Func<T, bool>> predicate, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync(predicate).Result
                        : db.Select(predicate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 通过SqlExpression lambda表达式返回数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected List<T> Select(Func<SqlExpression<T>, SqlExpression<T>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync(expression).Result
                        : db.Select(expression);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected List<Into> Select<Into, From>(SqlExpression<From> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync<Into, From>(expression).Result
                        : db.Select<Into, From>(expression);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected List<Into> Select<Into, From>(Func<SqlExpression<From>, SqlExpression<From>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.SelectAsync<Into, From>(expression).Result
                        : db.Select<Into, From>(expression);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected List<T> SelectByIds(IEnumerable idValues, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.SelectByIdsAsync<T>(idValues).Result
                        : db.SelectByIds<T>(idValues);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Single封装

        /// <summary>
        /// 通过lambda表达式返回单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected T Single(Expression<Func<T, bool>> predicate, bool async = false)
        {
            using (var db = OpenDbConnection())
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
        protected T Single(Func<SqlExpression<T>, SqlExpression<T>> expression, bool async = false)
        {
            using (var db = OpenDbConnection())
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
        protected T Single(SqlExpression<T> expression, bool async = false)
        {
            using (var db = OpenDbConnection())
            {
                return async
                    ? db.SingleAsync(expression).Result
                    : db.Single(expression);
            }
        }

        protected T SingleById(object idValue, bool async = false)
        {
            using (var db = OpenDbConnection())
            {
                return async
                    ? db.SingleByIdAsync<T>(idValue).Result
                    : db.SingleById<T>(idValue);
            }
        }

        protected T SingleWhere(string name, object value, bool async = false)
        {
            using (var db = OpenDbConnection())
            {
                return async
                    ? db.SingleWhereAsync<T>(name, value).Result
                    : db.SingleWhere<T>(name, value);
            }
        }

        #endregion

        #region Insert封装

        protected long Insert(T entity, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected void Insert(IEnumerable<T> entities, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected int Update(T entity, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected int UpdateNonDefaults(T entity, Expression<Func<T, bool>> where, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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


        protected int Update(object updateOnly, Expression<Func<T, bool>> where = null, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected int Delete(T entity, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected int Delete(Expression<Func<T, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected int DeleteById(object id, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.DeleteByIdAsync<T>(id).Result
                        : db.DeleteById<T>(id);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected int DeleteByIds(IEnumerable idValues, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.DeleteByIdsAsync<T>(idValues).Result
                        : db.DeleteByIds<T>(idValues);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        #endregion

        #region 功能函数

        /// <summary>
        /// 获取行计数
        /// </summary>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        protected long Count(bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.CountAsync<T>().Result
                        : db.Count<T>();
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
        protected long Count(Expression<Func<T, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected long Count(Func<SqlExpression<T>, SqlExpression<T>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected long Count(SqlExpression<T> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected long RowCount(SqlExpression<T> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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
        protected long RowCount(string sql, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected bool Exists(Expression<Func<T, bool>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected bool Exists(Func<SqlExpression<T>, SqlExpression<T>> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected bool Exists(SqlExpression<T> expression, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
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

        protected bool Exists(object anonType, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync<T>(anonType).Result
                        : db.Exists<T>(anonType);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        protected bool Exists(string sql, object anonType = null, bool async = false)
        {
            try
            {
                using (var db = OpenDbConnection())
                {
                    return async
                        ? db.ExistsAsync<T>(sql, anonType).Result
                        : db.Exists<T>(sql, anonType);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
