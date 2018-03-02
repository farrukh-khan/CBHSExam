using DataAccess.BLL;
using Mvc.ViewModel;
using Service.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Mvc.Common;

namespace Mvc.Controllers
{
    public class MemberController : AbstractController
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        // GET: Member
        public ActionResult Index(int? page)
        {

            int pageSize = 10;
            int pageNumber = (page ?? 1);
            int take = pageSize * pageNumber;

            var data = GetAllMembers(take, take - 10);
            
            return View(data.ToPagedList(pageNumber, pageSize));
        }


        [HttpPost]
        public ActionResult Edit(int id)
        {
            try
            {
                var member = _memberService.GetMemberById(id);
                var body = RenderViewToStringInternal("~/Views/Member/_NewUpdate.cshtml", member, true);
                return JsonResponse(ErrorMessage.RecordSave,
                 status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }







        [HttpPost]
        public ActionResult Save(Member model)
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

                var data = GetAllMembers(10, 0);
                var usersAsIPagedList = new StaticPagedList<Member>(data, 1, 10, 10);

                var body = RenderViewToStringInternal("~/Views/Member/_MemberGrid.cshtml", usersAsIPagedList, true);
                return JsonResponse(ErrorMessage.RecordSave,
                 status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            try
            {

                var member = _memberService.GetMemberById(id);

                if (member != null)
                {
                    member.IsActive = false;
                    _memberService.UpdateMember(member);
                }

                var data = GetAllMembers(10, 0);
                var usersAsIPagedList = new StaticPagedList<Member>(data, 1, 10, 10);

                var body = RenderViewToStringInternal("~/Views/Member/_MemberGrid.cshtml", usersAsIPagedList, true);
                return JsonResponse(ErrorMessage.RecordSave,
                 status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }



        private IEnumerable<Member> GetAllMembers(int take, int skip)
        {

            try
            {
                IEnumerable<Member> data = _memberService.GetMembers(x => x.IsActive).Take(take).Skip(skip).OrderBy(x => x.DateOfBirth);
                return data;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}