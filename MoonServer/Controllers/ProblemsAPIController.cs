using MoonServer.Models;
using MoonServer.Models.Proxy;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
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
        public string SubStatus;
        public string Message;
    }

    public class ChooseResponse
    {
        public HttpStatusCode Status;
        public string SubStatus;
        public string Message;
        public int Id;
    }

    public class ProblemsAPIController : ApiController
    {
        private static readonly MoonServerDB db = new MoonServerDB();
        private readonly TcpClient TcpClient = new TcpClient();
        private static readonly MoonboardClient moonboardClient
            = new MoonboardClient(IPAddress.Parse(Constants.GetFileConfig("MoonboardIP")),
                int.Parse(Constants.GetString("MoonboardPort")),
                db);

        // POST api/Problems
        [HttpPost]
        [Route("api/Problems")]
        public JsonResult<ProblemResponse> Post([FromBody] ProblemViewModel probVM)
        {
            Dictionary<string, string> filtVals = new Dictionary<string, string>();
            foreach (var f in Constants.Config.Filters)
            {
                string filtVal = (string)probVM.GetType().GetProperty(f.CSharpAttr).GetValue(probVM);
                if (filtVal == null) { filtVal = Constants.GetString("AllFilterName"); }
                filtVals.Add(f.Name, filtVal);
            }
            List<string> filtParts = new List<string>();
            foreach (var pair in filtVals.OrderBy(p => p.Key))
            {
                filtParts.Add(pair.Key + "_" + pair.Value);
            }
            string fname = Constants.GetFileConfig("CacheDir") + "\\" + string.Join("_", filtParts) + ".json";
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
        public JsonResult<ChooseResponse> Choose(int id)
        {
            try
            {
                moonboardClient.ShowProblem(id);
                return Json(new ChooseResponse { Status = HttpStatusCode.OK, Id = id });
            }
            catch (MoonboardClientException mbe)
            {
                return Json(new ChooseResponse
                {
                    Status = HttpStatusCode.InternalServerError,
                    Message = mbe.Message
                });
            }
        }
    }
}