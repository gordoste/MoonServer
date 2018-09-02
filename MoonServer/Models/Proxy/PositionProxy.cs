namespace MoonServer.Models.Proxy
{
    public class PositionProxy : Proxy
    {
        public long Id;
        public string Name;

        public PositionProxy() { }

        public PositionProxy(Position p)
        {
            Id = p.Id;
            Name = p.Name;
        }

        public override string GetDataType()
        {
            return "Position";
        }

        public override string FriendlyString()
        {
            return string.Format("{0}", Name);
        }
    }
}