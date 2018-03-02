using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DataAccess.Repository
{
    // <summary>
    /// Generic repository interface
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Add(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="predicate"></param>
        void Delete(Func<T, Boolean> predicate);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(long id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        T Get(Func<T, Boolean> where);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        IEnumerable<T> GetAll();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        IEnumerable<T> GetMany(Func<T, bool> where);
        List<T> GetManyIQueryable(Func<T, bool> where);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> GetBySp(string query, SqlParameter[] Parameters);
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TElement> SqlQuery<TElement>(string sql);
        /// <summary>
        /// 
        /// </summary>
        IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters);
        /// <summary>
        /// 
        /// </summary>
        int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters);

    }
}
