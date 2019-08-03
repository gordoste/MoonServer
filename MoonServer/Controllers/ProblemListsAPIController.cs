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
        [Route("api/ProblemLists/Get")]
        public JsonResult<ProblemListResponse> Get()
        {
            List<ProblemListViewModel> plList = new List<ProblemListViewModel>();
            foreach (ProblemList pl in db.ProblemLists)
            {
                plList.Add(new ProblemListViewModel { Id = pl.Id, Name = pl.Name, Count = pl.ProblemListEntries.Count });
            }
            return Json(new ProblemListResponse { Status = HttpStatusCode.OK, ProblemLists = plList });
        }

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

        [HttpGet]
        [Route("api/ProblemLists/{listId}/RemoveProblem/{probId}")]
        public JsonResult<Response> RemoveProblem(int listId, int probId)
        {
            ProblemListEntry ple = db.ProblemListEntries.First(p => p.ProblemId == probId && p.ProblemListId == listId);
            if (ple == null)
            {
                return Json(new Response { Status = HttpStatusCode.OK, Message = "Problem not on list" });
            }
            db.ProblemListEntries.Remove(ple);
            db.SaveChanges();

            return Json(new Response { Status = HttpStatusCode.OK, Message = "Removed" });
        }

        // Add a problem list
        [HttpPost]
        [Route("api/ProblemLists/Add")]
        public JsonResult<Response> Add([FromBody] string name)
        {
            ProblemList newList = db.ProblemLists.Create();
            newList.Name = name;
            db.ProblemLists.Add(newList);
            db.SaveChanges();
            return Json(new Response { Status = HttpStatusCode.OK });
        }

        [HttpGet]
        [Route("api/ProblemLists/Remove/{id}")]
        public JsonResult<Response> Remove(int id)
        {
            ProblemList pl = db.ProblemLists.First(p => p.Id == id);
            if (pl == null)
            {
                return Json(new Response { Status = HttpStatusCode.OK, Message = "List not found" });
            }
            db.ProblemLists.Remove(pl);
            db.SaveChanges();
            return Json(new Response { Status = HttpStatusCode.OK });
        }
    }
}