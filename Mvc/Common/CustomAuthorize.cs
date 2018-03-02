using Service.Contracts;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;


namespace Mvc.Common
{
    public class CustomAuthorize : System.Web.Mvc.AuthorizeAttribute
    {
     
        public CustomAuthorize()
        {
            
        }

        protected override bool AuthorizeCore(System.Web.HttpContextBase httpContext)
        {
            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {

            
            
        }
    }
}