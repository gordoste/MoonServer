using MoonServer.Models;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class BaseController : Controller
    {
        protected MoonServerDB db = new MoonServerDB();

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
        }
    }
}