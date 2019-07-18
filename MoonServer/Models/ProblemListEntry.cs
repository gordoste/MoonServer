namespace MoonServer.Models
{
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class ProblemListEntry
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public long ProblemId { get; set; }

        public long ProblemListId { get; set; }

        public virtual ProblemList ProblemList { get; set; }

        public virtual Problem Problem { get; set; }
    }
}
