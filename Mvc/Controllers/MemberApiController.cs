using DataAccess.BLL;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Mvc.Controllers
{
    public class MemberApiController : ApiController
    {


        private readonly IMemberService _memberService;

        public MemberApiController()
        {
            _memberService = StructureMap.ObjectFactory.GetInstance<IMemberService>();
        }

        [Route("api/Members")]
        public IHttpActionResult GetMembers(int? page)
        {
            try
            {

                int pageNumber = (page ?? 1);

                int take = 10 * pageNumber;

                IEnumerable<Member> data = _memberService.GetMembers(x => x.IsActive).Take(take).Skip(take - 10).OrderBy(x => x.DateOfBirth);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }



        [Route("api/MemberById")]
        public IHttpActionResult GetMemberById(int id)
        {
            try
            {
                Member data = _memberService.GetMemberById(id);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpPost]
        [Route("api/SaveMember")]
        public IHttpActionResult SaveMember(Member model)
        {
            try
            {

                if (model.MemberId > 0)
                {
                    var member = _memberService.GetMemberById(model.MemberId);
                    member.FirstName = model.FirstName;
                    member.LastName = model.LastName;
                    member.Email = model.Email;
                    member.DateOfBirth = model.DateOfBirth;
                    _memberService.UpdateMember(member);
                }
                else
                {
                    model.IsActive = true;
                    _memberService.InsertMember(model);
                }



                int pageNumber = 1;

                int take = 10 * pageNumber;

                IEnumerable<Member> data = _memberService.GetMembers(x => x.IsActive).Take(take).Skip(take - 10).OrderBy(x => x.DateOfBirth);
                return Ok(data);

            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

        [HttpGet]
        [Route("api/Delete")]
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var member = _memberService.GetMemberById(id);
                if (member != null)
                {
                    member.IsActive = false;
                    _memberService.UpdateMember(member);

                    int pageNumber = 1;

                    int take = 10 * pageNumber;

                    IEnumerable<Member> data = _memberService.GetMembers(x => x.IsActive).Take(take).Skip(take - 10).OrderBy(x => x.DateOfBirth);
                    return Ok(data);

                }
                else
                {
                    return ResponseMessage(Request.CreateResponse(HttpStatusCode.NotFound));
                }
            }
            catch (Exception ex)
            {
                return ResponseMessage(Request.CreateResponse(HttpStatusCode.InternalServerError, ex.Message));
            }
        }

    }
}
