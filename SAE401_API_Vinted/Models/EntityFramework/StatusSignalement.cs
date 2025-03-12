using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_statussignalement_sts")]
    public class StatusSignalement
    {
        [Key]
        [Column("sts_id")]
        public int StatusSignalementId { get; set; }

        [Required]
        [Column("sts_libelle")]
        [StringLength(15)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Signalement.StatusDuSignalement))]
        public virtual ICollection<Signalement> StatusDesSignalements { get; set; } = new List<Signalement>();
    }
}
