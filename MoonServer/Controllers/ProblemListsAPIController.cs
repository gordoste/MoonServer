using MoonServer.Models;
using MoonServer.Models.Proxy;
using System.Collections.Generic;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace MoonServer.Controllers
{
    public class ProblemListsAPIController : ApiController
    {
        private static readonly MoonServerDB db = new MoonServerDB();

        [HttpGet]
        [Route("api/ProblemLists/Choose/{id}")]
        public JsonResult<ProblemResponse> Choose(int id)
        {
            ProblemList pl = db.ProblemLists.Find(id);
            List<ProblemProxy> problems = new List<ProblemProxy>();
            foreach (ProblemListEntry ple in pl.ProblemListEntries)
            {
                problems.Add(new ProblemProxy(ple.Problem));
            }
            return Json(new ProblemResponse { Status = HttpStatusCode.OK, Problems = problems });
        }
    }
}