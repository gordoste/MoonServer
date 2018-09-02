using MoonServer.Models.Proxy;
using System.Linq;

namespace MoonServer.Models.Serialization
{
    // The proxies only have the attributes that will actually be read from/written to JSON
    // These are converted to/from the EF entities

    // Going from proxy to entity
    // Don't want to mess around to add extra constructor taking proxy as parameter to the entities, so use static class instead
    public static class Deproxy
    {
        public static Grade GetGrade(GradeProxy gp)
        {
            return new Grade
            {
                Id = gp.Id,
                EuroName = gp.EuroName,
                AmericanName = gp.AmericanName
            };
        }

        public static Hold GetHold(HoldProxy hp)
        {
            return new Hold
            {
                Id = hp.Id,
                Holdset = (int)Utils.ReadHoldsetString(hp.Holdset),
                Name = hp.Name
            };
        }

        public static HoldPlacement GetHoldPlacement(HoldPlacementProxy hpp, MoonServerDB moonServer)
        {
            return new HoldPlacement
            {
                Id = hpp.Id,
                HoldId = moonServer.Holds.First(h => h.Name.Equals(hpp.HoldName)).Id,
                PositionId = moonServer.Positions.First(p => p.Name.Equals(hpp.PositionName)).Id,
                Orientation = (int)Utils.ReadOrientationString(hpp.Orientation)
            };
        }

        public static HoldSetup GetHoldSetup(HoldSetupProxy hsp)
        {
            return new HoldSetup
            {
                Id = hsp.Id,
                Name = hsp.Name,
                HoldSetupHoldPlacements = hsp.HoldPlacements.ConvertAll(
                    hpId => new HoldSetupHoldPlacement { HoldPlacementId = hpId, HoldSetupId = hsp.Id })
            };
        }

        public static Position GetPosition(PositionProxy pp)
        {
            return new Position
            {
                Id = pp.Id,
                Name = pp.Name
            };
        }

        public static Problem GetProblem(ProblemProxy pp, MoonServerDB moonServer)
        {
            return new Problem
            {
                Id = pp.Id,
                MoonID = pp.MoonID,
                Name = pp.Name,
                IsBenchmark = pp.IsBenchmark,
                GradeId = moonServer.Grades.First(g => g.EuroName.Equals(pp.GradeName)).Id,
                HoldSetupId = moonServer.HoldSetups.First(hs => hs.Name.Equals(pp.HoldSetupName)).Id,
                Repeats = pp.Repeats,
                DateAdded = pp.DateAdded,
                Rating = pp.Rating
            };
        }
    }
}