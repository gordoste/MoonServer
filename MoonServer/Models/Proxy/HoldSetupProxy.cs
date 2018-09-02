using System.Collections.Generic;

namespace MoonServer.Models.Proxy
{
    public class HoldSetupProxy : Proxy
    {
        public long Id;
        public string Name;
        public List<long> HoldPlacements = new List<long>();

        public HoldSetupProxy() { }

        public HoldSetupProxy(HoldSetup _hs)
        {
            Id = _hs.Id;
            Name = _hs.Name;
            HoldPlacements = new List<HoldSetupHoldPlacement>(_hs.HoldSetupHoldPlacements)
                .ConvertAll(hshp => hshp.Id);
        }

        public override string GetDataType()
        {
            return "HoldSetup";
        }

        public override string FriendlyString()
        {
            return string.Format("{0}", Name);
        }
    }
}