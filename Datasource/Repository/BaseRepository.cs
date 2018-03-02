using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using DataAccess.BLL;
using DataAccess.DAL;
using System.Data.Entity.Infrastructure;


namespace DataAccess.Repository
{
    /// <summary>
    /// Abstract Entity Framework repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseRepository<T> : IRepository<T>
        where T : class
    {
        private DataContext _context;
        private readonly DbSet<T> _dbset;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFactory"></param>
        protected BaseRepository(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            _dbset = Context.Set<T>();
        }
        /// <summary>
        /// 
        /// </summary>
        protected IDbFactory DbFactory
        {
            get;
            private set;
        }


        /// <summary>
        /// 
        /// </summary>
        protected DbContext Context
        {
            get
            {
                return _context ?? (_context = DbFactory.Get());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Add(T entity)
        {
            _dbset.Add(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Update(T entity)
        {
            _context.Entry(entity).CurrentValues.SetValues(System.Data.Entity.EntityState.Modified);

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        public virtual void Delete(T entity)
        {
            _dbset.Remove(entity);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        public void Delete(Func<T, Boolean> where)
        {
            IEnumerable<T> objects = _dbset.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                _dbset.Remove(obj);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(long id)
        {
            return _dbset.Find(id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            return _dbset.ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> GetMany(Func<T, bool> where)
        {
            return _dbset.Where(where);
        }
        public virtual List<T> GetManyIQueryable(Func<T, bool> where)
        {
            return _dbset.Where(where).ToList();
        }
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="where"></param>
        /// <returns></returns>
        public T Get(Func<T, Boolean> where)
        {
            return _dbset.Where(where).FirstOrDefault<T>();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> GetBySp(string query, SqlParameter[] Parameters)
        {
            return _context.Database.SqlQuery<T>(query, Parameters);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql)
        {
            return _context.Database.SqlQuery<TElement>(sql);
        }

        public IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            return _context.Database.SqlQuery<TElement>(sql, parameters);
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            int? previousTimeout = null;
            if (timeout.HasValue)
            {
                //store previous timeout
                previousTimeout = ((IObjectContextAdapter)this).ObjectContext.CommandTimeout;
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = timeout;
            }

            var transactionalBehavior = doNotEnsureTransaction
                ? TransactionalBehavior.DoNotEnsureTransaction
                : TransactionalBehavior.EnsureTransaction;
            var result = _context.Database.ExecuteSqlCommand(transactionalBehavior, sql, parameters);

            if (timeout.HasValue)
            {
                //Set previous timeout back
                ((IObjectContextAdapter)this).ObjectContext.CommandTimeout = previousTimeout;
            }

            //return result
            return result;
        }


    }
}
