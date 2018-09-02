namespace MoonServer.Models.Proxy
{
    public class GradeProxy : Proxy
    {
        public long Id;
        public string EuroName;
        public string AmericanName;

        public GradeProxy() { }

        public GradeProxy(Grade g)
        {
            Id = g.Id;
            EuroName = g.EuroName;
            AmericanName = g.AmericanName;
        }

        public override string GetDataType()
        {
            return "Grade";
        }

        public override string FriendlyString()
        {
            return string.Format("{0}", AmericanName);
        }
    }
}