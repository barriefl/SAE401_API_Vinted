using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_statusoffre_tso")]
    public class StatusOffre
    {
        [Key]
        [Column("tso_id")]
        public int StatusOffreId { get; set; }

        [Required]
        [Column("tso_libelle")]
        [StringLength(20)]
        public string StatusOffreLibelle { get; set; } = null!;

        [InverseProperty(nameof(Offre.EstStatusOffre))]
        public virtual ICollection<Offre> StatusOffres { get; set; } = new List<Offre>();
    }
}
