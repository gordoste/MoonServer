namespace MoonServer.Models
{
    using MoonServer.Models.Proxy;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Web.Script.Serialization;

    public partial class ProblemList
    {
        private JavaScriptSerializer JSONSerializer = new JavaScriptSerializer();

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ProblemList()
        {
            ProblemListEntries = new HashSet<ProblemListEntry>();
        }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ProblemListEntry> ProblemListEntries { get; set; }

        public string PropertiesAsJson()
        {
            return JSONSerializer.Serialize(new ProblemListProxy(this));
        }
    }
}