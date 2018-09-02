using MoonServer.Models;
using System;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace MoonServer
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // Initialise the cache entry
            HttpRuntime.Cache.Insert(Constants.CacheKey, DateTime.Now);
            using (MoonServerDB db = new MoonServerDB())
            {
                HttpRuntime.Cache.Insert(Constants.GradeKey, db.Grades.ToList().ConvertAll(g => g.AmericanName));
            }

            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
