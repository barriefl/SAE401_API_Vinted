using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_matiere_mat")]
    public class Matiere
    {
        [Key]
        [Column("mat_id")]
        public int MatiereId { get; set; }

        [Column("mat_caracteristique")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieID { get; set; }

        [Required]
        [Column("mat_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Caracteristique.CaracteristiqueMatiere))]
        public virtual Caracteristique CaracteristiqueDeMatiere { get; set; } = null!;
    }
}
