using MoonServer.Models;
using MoonServer.Models.Proxy;
using System.Collections.Generic;
using System.Net;

namespace MoonServer.Controllers
{
    public class Response
    {
        public HttpStatusCode Status;
        public string SubStatus;
        public string Message;
    }

    public class ProblemResponse : Response
    {
        public IEnumerable<ProblemProxy> Problems;
    }

    public class ProblemListResponse : Response
    {
        public IEnumerable<ProblemListViewModel> ProblemLists;
    }
}