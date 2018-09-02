namespace MoonServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

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
