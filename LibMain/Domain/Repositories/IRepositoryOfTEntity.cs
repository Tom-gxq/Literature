using LibMain.Domain.Entities;
using ServiceStack.OrmLite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LibMain.Domain.Repositories
{
    /// <summary>
    /// A shortcut of <see cref="IRepository{TEntity,TPrimaryKey}"/> for most used primary key type (<see cref="int"/>).
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public interface IRepository<TEntity> : IRepository<TEntity, int> where TEntity : class, IEntity<int>
    {
        #region Select封装
        /// <summary>
        /// 通过lambda表达式返回数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        List<TEntity> Select(Expression<Func<TEntity, bool>> predicate, bool async = false);

        /// <summary>
        /// 通过SqlExpression lambda表达式返回数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        List<TEntity> Select(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false);

        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        List<Into> Select<Into, From>(SqlExpression<From> expression, bool async = false);

        /// <summary>
        /// 联接表结果
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        List<Into> Select<Into, From>(Func<SqlExpression<From>, SqlExpression<From>> expression, bool async = false);

        List<TEntity> SelectByIds(IEnumerable idValues, bool async = false);
        #endregion

        #region Single封装
        /// <summary>
        /// 通过lambda表达式返回单条数据
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        TEntity Single(Expression<Func<TEntity, bool>> predicate, bool async = false);

        /// <summary>
        /// 通过SqlExpression lambda表达式返回单条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        TEntity Single(Func<SqlExpression<TEntity>, SqlExpression<TEntity>> expression, bool async = false);

        /// <summary>
        /// 通过SqlExpression lambda表达式返回单条数据
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="async">默认异步</param>
        /// <returns></returns>
        TEntity Single(SqlExpression<TEntity> expression, bool async = false);

        TEntity SingleById(object idValue, bool async = false);

        TEntity SingleWhere(string name, object value, bool async = false);
        #endregion

        #region Insert封装
        long Insert(TEntity entity, bool async = false);

        void Insert(IEnumerable<TEntity> entities, bool async = false);
        #endregion

        #region Update封装
        /// <summary>
        /// 更新所有的字段，未赋值字段则为默认值
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        int Update(TEntity entity, bool async = false);

        /// <summary>
        /// 更新所提供的字段，对象的int，bool必须设置为可空，否则设置int=0会失败
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="where"></param>
        /// <param name="async"></param>
        /// <returns></returns>
        int UpdateNonDefaults(TEntity entity, Expression<Func<TEntity, bool>> where, bool async = false);


        int Update(object updateOnly, Expression<Func<TEntity, bool>> where = null, bool async = false);
        #endregion

        #region Delete封装

        int Delete(TEntity entity, bool async = false);

        int Delete(Expression<Func<TEntity, bool>> expression, bool async = false);

        int DeleteById(object id, bool async = false);

        int DeleteByIds(IEnumerable idValues, bool async = false);

        #endregion
    }
}
