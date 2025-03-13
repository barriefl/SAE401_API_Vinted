using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeavis_tas")]
    public class TypeAvis
    {
        [Key]
        [Column("tas_id")]
        public int Code { get; set; }

        [Required]
        [Column("tas_lib")]
        [StringLength(40)]
        public int Libelle { get; set; }

        [InverseProperty(nameof(Avis.APourTypeAvis))]
        public virtual ICollection<Avis> PossedesTypeAvis { get; set; } = new List<Avis>();
    }
}
