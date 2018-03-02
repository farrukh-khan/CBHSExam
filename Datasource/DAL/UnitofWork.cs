using DataAccess.BLL;
using System;

namespace DataAccess.DAL
{
    public class UnitofWork : IUnitofWork
    {
        private readonly IDbFactory _dbFactory;
        private DataContext _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbFactory"></param>
        public UnitofWork(IDbFactory dbFactory)
        {
            _dbFactory = dbFactory;
        }

        /// <summary>
        /// 
        /// </summary>
        protected DataContext Context
        {
            get { return _context ?? (_context = _dbFactory.Get()); }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Commit()
        {
            try
            {
                Context.SaveChanges();
            }
            catch (System.Data.Entity.Validation.DbEntityValidationException dbEx)
            {
                Exception raise = dbEx;
                foreach (var validationErrors in dbEx.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                    {
                        string message = string.Format("{0}:{1}",
                            validationErrors.Entry.Entity.ToString(),
                            validationError.ErrorMessage);
                        raise = new InvalidOperationException(message, raise);
                    }
                }
                throw raise;
            }
        }
    }
}
