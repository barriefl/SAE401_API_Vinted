using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeavis_tas")]
    public class TypeAvis
    {
        [Key]
        [Column("tas_id")]
        public int TypeAvisID { get; set; }

        [Required]
        [Column("tas_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Avis.APourTypeAvis))]
        public virtual ICollection<Avis> PossedesTypeAvis { get; set; } = new List<Avis>();
    }
}
