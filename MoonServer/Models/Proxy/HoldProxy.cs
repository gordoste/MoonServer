namespace MoonServer.Models.Proxy
{
    public class HoldProxy : Proxy
    {
        public long Id;
        public string Holdset;
        public string Name;

        public HoldProxy() { }

        public HoldProxy(Hold h)
        {
            Id = h.Id;
            Holdset = Utils.HoldsetAsString((Holdset)h.Holdset);
            Name = h.Name;
        }

        public override string GetDataType()
        {
            return "Hold";
        }

        public override string FriendlyString()
        {
            return string.Format("#{0} ({1})", Name, Holdset);
        }
    }
}