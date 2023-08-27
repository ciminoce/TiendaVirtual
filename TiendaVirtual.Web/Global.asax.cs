using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using TiendaVirtual.Web.App_Start;
using TiendaVirtual.Web.Helpers;

namespace TiendaVirtual.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            CreateRolesAndSuperUser();
            AutoMapperConfig.Initialize();
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
        private void CreateRolesAndSuperUser()
        {
            UsersHelper.CheckRole("Admin");
            UsersHelper.CheckRole("Cliente");
            UsersHelper.CheckSuperUser();
        }

    }
}
