namespace MoonServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class HoldPlacement
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public HoldPlacement()
        {
            HoldSetupHoldPlacements = new HashSet<HoldSetupHoldPlacement>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long HoldId { get; set; }

        public long PositionId { get; set; }

        public int Orientation { get; set; }

        public virtual Hold Hold { get; set; }

        public virtual Position Position { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<HoldSetupHoldPlacement> HoldSetupHoldPlacements { get; set; }
    }
}
