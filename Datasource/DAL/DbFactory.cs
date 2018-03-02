using System;
using DataAccess.BLL;

namespace DataAccess.DAL
{
    public class DbFactory : IDbFactory
    {
        private DataContext _context;


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public DataContext Get()
        {
            return _context ?? (_context = new DataContext());
        }

        #region Dispose

        private bool _disposed;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this._disposed = true;
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
