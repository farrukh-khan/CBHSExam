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
using System.Net.Http;
using System.Threading.Tasks;
using System.Configuration;

namespace Mvc.Controllers
{
    public class HomeController : AbstractController
    {

        private string _BaseUrl = ConfigurationManager.AppSettings["BaseUrl"];


        //GET: Member
        public async Task<ActionResult> Index(int? page)
        {


            int pageNumber = (page ?? 1);
            IEnumerable<Member> members = null;

            string apiUrl = string.Format("{0}/api/Members?page={1}", _BaseUrl, page);
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apiUrl);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync(apiUrl);
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    members = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Member>>(data);
                }
            }


            int pageSize = 10;
            return View(members.ToPagedList(pageNumber, pageSize));
        }




        [HttpPost]
        public async Task<ActionResult> Edit(int id)
        {
            try
            {
                Member member = null;

                string apiUrl = string.Format("{0}/api/MemberById?id={1}", _BaseUrl, id);
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        member = Newtonsoft.Json.JsonConvert.DeserializeObject<Member>(data);
                    }
                }

                var body = RenderViewToStringInternal("~/Views/Home/_NewUpdate.cshtml", member, true);


                return JsonResponse(ErrorMessage.RecordSave, status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }







        [HttpPost]
        public async Task<ActionResult> Save(Member model)
        {
            try
            {

                IEnumerable<Member> members = null;
                string apiUrl = string.Format("{0}/api/SaveMember", _BaseUrl);
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    var postData = new List<KeyValuePair<string, string>>();
                    postData.Add(new KeyValuePair<string, string>("MemberId", model.MemberId.ToString()));
                    postData.Add(new KeyValuePair<string, string>("FirstName", model.FirstName));
                    postData.Add(new KeyValuePair<string, string>("LastName", model.LastName));
                    postData.Add(new KeyValuePair<string, string>("Email", model.Email));
                    postData.Add(new KeyValuePair<string, string>("DateOfBirth", model.DateOfBirth.ToString()));

                    var content = new FormUrlEncodedContent(postData);
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        members = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Member>>(data);
                    }
                }


                var usersAsIPagedList = new StaticPagedList<Member>(members, 1, 10, 10);

                var body = RenderViewToStringInternal("~/Views/Home/_MemberGrid.cshtml", usersAsIPagedList, true);
                return JsonResponse(ErrorMessage.RecordSave,
                 status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {


                IEnumerable<Member> members = null;
                string apiUrl = string.Format("{0}/api/Delete?id={1}", _BaseUrl, id);
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(apiUrl);
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var data = await response.Content.ReadAsStringAsync();
                        members = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<Member>>(data);
                    }
                }


                var usersAsIPagedList = new StaticPagedList<Member>(members, 1, 10, 10);

                var body = RenderViewToStringInternal("~/Views/Home/_MemberGrid.cshtml", usersAsIPagedList, true);
                return JsonResponse(ErrorMessage.RecordSave,
                 status: AjaxResponseStatus.Success, html: body);
            }
            catch (Exception ex)
            {
                return JsonResponse(ex.Message, status: AjaxResponseStatus.Error);
            }


        }




    }
}