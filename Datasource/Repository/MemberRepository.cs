using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;

using DataAccess.BLL;

namespace DataAccess.Repository
{
    public class MemberRepository : BaseRepository<Member>, IMemberRepository
    {
        public MemberRepository(IDbFactory dbFactory)
            : base(dbFactory)
        {
        }
    }
}
