using $safeprojectname$.Areas.IdentityManagement.Helpers;
using $safeprojectname$.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace $safeprojectname$
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            $safeprojectname$DbContext context = new $safeprojectname$DbContext();
            IdentityHelper.SeedIdentities(context);
        }
    }
}
