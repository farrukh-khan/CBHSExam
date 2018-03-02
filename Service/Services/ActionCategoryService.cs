using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.BLL;
using DataAccess.DAL;
using DataAccess.RepositoryContracts;
using Service.Contracts;

namespace Service.Services
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IUnitofWork _unitOfWork;

        public MemberService(IMemberRepository MemberRepository, IUnitofWork unitOfWork)
        {
            _memberRepository = MemberRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Member> GetMembers()
        {
            return _memberRepository.GetAll();
        }

        public IEnumerable<Member> GetMembers(Func<Member, bool> where)
        {
            if (@where == null) throw new ArgumentException("where");
            return _memberRepository.GetMany(where);
        }

        public Member GetMemberById(int id)
        {
            return _memberRepository.GetById(id);
        }

        public void InsertMember(Member MemberObj)
        {
            _memberRepository.Add(MemberObj);
            _unitOfWork.Commit();
        }

        public void UpdateMember(Member MemberObj)
        {
            _memberRepository.Update(MemberObj);
            _unitOfWork.Commit();
        }

        public void DeleteMember(Member MemberObj)
        {
            _memberRepository.Delete(MemberObj);
            _unitOfWork.Commit();
        }
    }
}
