using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_motifsignalement_mos")]
    public class MotifSignalement
    {
        [Key]
        [Column("mos_id")]
        public int MotifSignalementId { get; set; }

        [Required]
        [Column("mos_libelle")]
        [StringLength(500)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Signalement.MotifDuSignalement))]
        public virtual ICollection<Signalement> MotifsDesSignalement { get; set; } = new List<Signalement>();
    }
}
