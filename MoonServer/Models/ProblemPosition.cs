namespace MoonServer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

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
