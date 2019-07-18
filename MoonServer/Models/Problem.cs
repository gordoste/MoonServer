namespace MoonServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Problem
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Problem()
        {
            EndProblemPositions = new HashSet<EndProblemPosition>();
            ProblemPositions = new HashSet<ProblemPosition>();
            StartProblemPositions = new HashSet<StartProblemPosition>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public int MoonID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        public bool IsBenchmark { get; set; }

        public long GradeId { get; set; }

        public long HoldSetupId { get; set; }

        public int Repeats { get; set; }

        public DateTime DateAdded { get; set; }

        public int Rating { get; set; }

        public virtual Grade Grade { get; set; }

        public virtual HoldSetup HoldSetup { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProblemPosition> ProblemPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<StartProblemPosition> StartProblemPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<EndProblemPosition> EndProblemPositions { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProblemListEntry> ProblemListEntries { get; set; }
    }
}
