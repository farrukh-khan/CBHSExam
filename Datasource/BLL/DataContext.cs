using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BLL
{

    public class DataContext : DbContext
    {
        public DbSet<Member> Members { get; set; }
    }
}
