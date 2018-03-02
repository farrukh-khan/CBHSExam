using System;
using DataAccess.BLL;

namespace DataAccess.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public interface IDbFactory : IDisposable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        DataContext Get();
    }
}
