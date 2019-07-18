using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace MoonServer.Controllers
{
    public class ProblemsController : BaseController
    {
        // GET: Problems
        public async Task<ActionResult> Index()
        //public ActionResult Index()
        {
            //    var problems = db.Problems.Include(p => p.Grade).Include(p => p.HoldSetup);
            //    return View(await problems.ToListAsync());
            var problemLists = db.ProblemLists;
            return View(await problemLists.ToListAsync());
        }
    }
}
