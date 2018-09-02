namespace MoonServer.Models.Proxy
{
    public class HoldPlacementProxy : Proxy
    {
        public long Id;
        public string HoldName;
        public string PositionName;
        public string Orientation;

        public HoldPlacementProxy() { }

        public HoldPlacementProxy(HoldPlacement _hp)
        {
            Id = _hp.Id;
            HoldName = _hp.Hold.Name;
            PositionName = _hp.Position.Name;
            Orientation = Utils.OrientationAsString((Orientation)_hp.Orientation);
        }

        public override string GetDataType()
        {
            return "HoldPlacement";
        }

        public override string FriendlyString()
        {
            return string.Format("{0}/{1}/{2}", HoldName, PositionName, Orientation);
        }
    }
}