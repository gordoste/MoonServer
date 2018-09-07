using MoonServer.Models;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class BaseController : Controller
    {
        protected MoonServerDB db = new MoonServerDB();

    }
}