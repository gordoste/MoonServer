using MoonServer.Models.Proxy;
using System.Collections.Generic;
using System.Net;

namespace MoonServer.Controllers
{
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
}