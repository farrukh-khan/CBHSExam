using Mvc.App_Start;
using Mvc.DI;
using Mvc.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace Mvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            RegisterDependencyResolution();
            InitializeAutoMapper();

            

            //GlobalConfiguration.Configure(WebApiConfig.Register);


            //HttpConfiguration config = new HttpConfiguration();
            //config.Formatters.JsonFormatter
            //            .SerializerSettings
            //            .ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;

            //WebApiConfig.Register(config);

        }


        /// <summary>
        /// 
        /// </summary>
        private void RegisterDependencyResolution()
        {
            var container = IoC.Initialize();
            DependencyResolver.SetResolver(new SmDependencyResolver(container));
        }


        private void InitializeAutoMapper()
        {
            // initialize automapper
            Type maperInterfaceType = typeof(IAutoMap);
            var mappers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(p => maperInterfaceType.IsAssignableFrom(p) && !p.IsInterface)
                .Select(x => (IAutoMap)Activator.CreateInstance(x))
                .ToList();

            mappers.ForEach(x => x.Initialize());
        }

        /// <summary>
        /// 
        /// </summary>
        public override void Init()
        {
            this.PostAuthenticateRequest += Agent_PostAuthenticateRequest;
            base.Init();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Agent_PostAuthenticateRequest(object sender, EventArgs e)
        {

        }


        public void Application_Error(Object sender, EventArgs e)
        {

        }


    }
}
