using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BLL;

namespace Service.Contracts
{
    public interface IMemberService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Member> GetMembers();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<Member> GetMembers(Func<Member, bool> where);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Member GetMemberById(int id);
        void InsertMember(Member MemberObj);
        void UpdateMember(Member MemberObj);
        void DeleteMember(Member MemberObj);
    }
}
