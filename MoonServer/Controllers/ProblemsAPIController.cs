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
    public class ProblemViewModel
    {
        public string Grade { get; set; }
        public string Rating { get; set; }
        public string Repeats { get; set; }
        public string Benchmark { get; set; }
    }

    public class ProblemResponse
    {
        public IEnumerable<ProblemProxy> Problems;
        public HttpStatusCode Status;
        public string Message;
    }

    public class ProblemsAPIController : ApiController
    {
        // POST api/Problems
        [HttpPost]
        [Route("api/Problems")]
        public JsonResult<ProblemResponse> Post([FromBody] ProblemViewModel probVM)
        {
            Dictionary<string, string> filtVals = new Dictionary<string, string>();
            foreach (var f in Constants.config.Filters)
            {
                string filtVal = (string)probVM.GetType().GetProperty(f.CSharpAttr).GetValue(probVM);
                if (filtVal == null) { filtVal = Constants.getString("AllFilterName"); }
                filtVals.Add(f.Name, filtVal);
            }
            List<string> filtParts = new List<string>();
            foreach (var pair in filtVals.OrderBy(p => p.Key))
            {
                filtParts.Add(pair.Key + "_" + pair.Value);
            }
            string fname = Constants.getString("CacheDir") + "\\" +
                string.Join("_", filtParts) + ".json";
            if (!File.Exists(fname))
            {
                return Json(new ProblemResponse { Status = HttpStatusCode.NotFound, Message = "Cannot find file." });
            }
            IEnumerable<ProblemProxy> problems = JsonConvert.DeserializeObject<List<ProblemProxy>>(File.ReadAllText(fname));
            if (problems.Count() > int.Parse(Constants.getString("MaxProblemsReturned")))
            {
                return Json(new ProblemResponse { Status = HttpStatusCode.Forbidden, Message = Constants.getString("TooManyProblemsMsg") });
            }
            return Json(new ProblemResponse { Status = HttpStatusCode.OK, Problems = problems });
        }
    }
}