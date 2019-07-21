namespace MoonServer.Models.Proxy
{
    public class ProblemListProxy : Proxy
    {
        public long Id;
        public string Name;

        public ProblemListProxy() { }

        public ProblemListProxy(ProblemList p)
        {
            Id = p.Id;
            Name = p.Name;
        }

        public override string GetDataType()
        {
            return "ProblemList";
        }

        public override string FriendlyString()
        {
            return string.Format("{0}", Name);
        }

    }
}