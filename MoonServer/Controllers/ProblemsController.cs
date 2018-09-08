using MoonServer.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class ProblemsController : BaseController
    {
        // GET: Problems
        //public async Task<ActionResult> Index()
        public ActionResult Index()
        {
            //    var problems = db.Problems.Include(p => p.Grade).Include(p => p.HoldSetup);
            //    return View(await problems.ToListAsync());
            return View(new List<Problem>());
        }
    }
}
