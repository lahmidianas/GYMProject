using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using Unity;
using Unity.Mvc5;
using GYMProject.Data;
using GYMProject.Repositories;

namespace GYMProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            

            var container = new UnityContainer();
            container.RegisterType<GYMContext>();
            container.RegisterType(typeof(IRepository<>), typeof(Repository<>));
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
        protected void Application_PostAuthenticateRequest()
        {
            if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated)
            {
                var username = System.Web.HttpContext.Current.User.Identity.Name;
                var roles = new[] { "Admin" }; // Adjust roles as needed
                System.Web.HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(new System.Security.Principal.GenericIdentity(username), roles);
            }
        }
    }
}
