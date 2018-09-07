namespace MoonServer.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Grade
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Grade()
        {
            Problems = new HashSet<Problem>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string EuroName { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string AmericanName { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Problem> Problems { get; set; }
    }

    public class AmericanGradeComparer : IComparer<string>
    {
        private bool Ascending { get; set; }
        public AmericanGradeComparer(bool Ascending = true)
        {
            this.Ascending = Ascending;
        }
        public int Compare(string x, string y)
        {
            if (x.Substring(0, 1) == "V" && y.Substring(0, 1) == "V")
            {
                int gx = int.Parse(x.Substring(1));
                int gy = int.Parse(y.Substring(1));
                return Ascending ? (gx - gy) : (gy - gx);
            }
            return StringComparer.InvariantCulture.Compare(x, y);
        }
    }
}
