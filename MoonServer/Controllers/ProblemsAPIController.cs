using MoonServer.Models;
using MoonServer.Models.Proxy;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Results;

namespace MoonServer.Controllers
{
    public class ProblemsAPIController : ApiController
    {
        private static readonly MoonServerDB db = new MoonServerDB();
        private static readonly MoonboardClient moonboardClient
            = new MoonboardClient(db);

        // POST api/Problems
        [HttpPost]
        [Route("api/Problems")]
        public JsonResult<ProblemResponse> Post([FromBody] ProblemViewModel probVM)
        {
            Dictionary<string, string> filtVals = new Dictionary<string, string>();
            // Map the JSON attribute to the CSharp properties - like a primitive ORM :)
            foreach (var f in Constants.Config.Filters)
            {
                // Confusing, but we go from the instance up to the class, then identify which property we want,
                // and request the value of that property for this instance!
                string filtVal = (string)probVM.GetType().GetProperty(f.CSharpAttr).GetValue(probVM);
                if (filtVal == null) { filtVal = Constants.GetString("AllFilterName"); } // If not specified, get everything
                filtVals.Add(f.Name, filtVal);
            }
            // Build the filename that has the data we want
            List<string> filtParts = new List<string>();
            foreach (var pair in filtVals.OrderBy(p => p.Key))
            {
                filtParts.Add(pair.Key + "_" + pair.Value);
            }
            string fname = Constants.GetFileConfig("CacheDir") + "\\" + string.Join("_", filtParts) + ".json";
            // Read the file and return the contents (de-serialising and re-serialising is needed to check whether
            // we are returning too many problems)
            if (!File.Exists(fname))
            {
                return Json(new ProblemResponse { Status = HttpStatusCode.NotFound, Message = "Cannot find file." });
            }
            IEnumerable<ProblemProxy> problems = JsonConvert.DeserializeObject<List<ProblemProxy>>(File.ReadAllText(fname));
            if (problems.Count() > int.Parse(Constants.GetString("MaxProblemsReturned")))
            {
                return Json(new ProblemResponse
                {
                    Status = HttpStatusCode.Forbidden,
                    SubStatus = Constants.GetString("TooManyProblemsCode"),
                    Message = string.Format("Too many problems ({0}), apply more filters", problems.Count())
                });
            }
            return Json(new ProblemResponse { Status = HttpStatusCode.OK, Problems = problems });
        }

        [HttpGet]
        [Route("api/Problems/Choose/{id}")]
        public JsonResult<Response> Choose(int id)
        {
            try
            {
                moonboardClient.ShowProblem(id);
                return Json(new Response { Status = HttpStatusCode.OK });
            }
            catch (MoonboardClientException mbe)
            {
                return Json(new Response
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = mbe.Message
                });
            }
        }
    }
}