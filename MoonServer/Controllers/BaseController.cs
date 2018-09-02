using MoonServer.Models;
using System;
using System.Web;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class BaseController : Controller
    {
        protected MoonServerDB db = new MoonServerDB();

        // Add a dependency on a magic CacheKey for every request
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.HttpContext.Response.AddCacheItemDependency(Constants.CacheKey);
        }

        // Update the magic cache key so that cached results are invalid
        protected void InvalidateCache()
        {
            HttpRuntime.Cache.Insert(Constants.CacheKey, DateTime.Now);
        }
    }
}