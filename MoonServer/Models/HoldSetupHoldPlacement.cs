namespace MoonServer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class HoldSetupHoldPlacement
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long HoldSetupId { get; set; }

        public long HoldPlacementId { get; set; }

        public virtual HoldPlacement HoldPlacement { get; set; }

        public virtual HoldSetup HoldSetup { get; set; }
    }
}
