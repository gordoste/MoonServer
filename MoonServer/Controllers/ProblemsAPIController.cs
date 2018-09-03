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

    public class ProblemsAPIController : ApiController
    {
        // POST api/Problems
        [HttpPost]
        [Route("api/Problems")]
        public JsonResult<IEnumerable<ProblemProxy>> Post([FromBody] ProblemViewModel probVM)
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
            IEnumerable<ProblemProxy> problems = JsonConvert.DeserializeObject<List<ProblemProxy>>(File.ReadAllText(fname));
            return Json(problems);
        }
    }
}