using System;
using System.IO;
using System.Web.Mvc;

namespace Mvc.Common
{
    public class ControllerBase : BaseController
    {
        protected String RenderPartialToString(String viewName, Object model)
        {
            if (String.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewDataDictionary ViewData = new ViewDataDictionary();
            TempDataDictionary TempData = new TempDataDictionary();
            ViewData.Model = model;

            using (StringWriter sw = new StringWriter())
            {
                ViewEngineResult viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);

                return sw.GetStringBuilder().ToString();
            }
        }

        protected string RenderViewToStringInternal(string viewPath, object model, bool partial = false)
        {
            // first find the ViewEngine for this view
            ViewEngineResult viewEngineResult = null;
            if (partial)
                viewEngineResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewPath);
            else
                viewEngineResult = ViewEngines.Engines.FindView(ControllerContext, viewPath, null);

            //if (viewEngineResult == null)
            //    throw new FileNotFoundException(Resources.ViewCouldNotBeFound);

            // get the view and attach the model to view data
            var view = viewEngineResult.View;
            ControllerContext.Controller.ViewData.Model = model;

            string result = null;

            using (var sw = new StringWriter())
            {
                var ctx = new ViewContext(ControllerContext, view,
                                            ControllerContext.Controller.ViewData,
                                            ControllerContext.Controller.TempData,
                                            sw);
                view.Render(ctx, sw);
                result = sw.ToString();
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="status"></param>
        /// <param name="message"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static JsonResult JsonResponse(string message, string details = "", AjaxResponseStatus status = AjaxResponseStatus.Warning, string html = "", string redirectUrl = "")
        {
            return new JsonResult
            {
                Data = AjaxResponse.GetResponse(message, details, status, html = html, redirectUrl = redirectUrl),
                ContentType = "application/json",
                JsonRequestBehavior = JsonRequestBehavior.AllowGet
            };
        }
    }
}