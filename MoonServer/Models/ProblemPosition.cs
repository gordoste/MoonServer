namespace MoonServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class ProblemPosition
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProblemId { get; set; }

        public long PositionId { get; set; }

        public virtual Position Position { get; set; }

        public virtual Problem Problem { get; set; }
    }
}
