using MoonServer.Models;
using MoonServer.Models.Proxy;
using System.Collections.Generic;
using System.Linq;
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

        [HttpGet]
        [Route("api/ProblemLists/{listId}/AddProblem/{probId}")]
        public JsonResult<Response> AddProblem(int listId, int probId)
        {
            ProblemList pl = db.ProblemLists.First(p => p.Id == listId);
            if (pl == null)
            {
                return Json(new Response { Status = HttpStatusCode.BadRequest, Message = "Invalid list ID" });
            }
            if (pl.ProblemListEntries.Where(p => p.ProblemId == probId).Count() > 0)
            {
                return Json(new Response { Status = HttpStatusCode.OK, Message = "Problem already on list" });
            }
            ProblemListEntry ple = db.ProblemListEntries.Create();
            ple.ProblemListId = listId;
            ple.ProblemId = probId;
            pl.ProblemListEntries.Add(ple);
            db.SaveChanges();

            return Json(new Response { Status = HttpStatusCode.OK, Message = "Added" });
        }
    }
}