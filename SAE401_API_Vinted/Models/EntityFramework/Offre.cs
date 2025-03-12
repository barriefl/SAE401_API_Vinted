using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_offre_ofr")]
    public class Offre : Message
    {
        [Required]
        [Range(0, 9999.99)]
        [Column("ofr_montant", TypeName = "numeric(6,2)")]
        public double OffreMontant { get; set; }

        [Required]
        [Column("tso_id")]
        public int TypeStatusOffreId { get; set; }

        [ForeignKey(nameof(TypeStatusOffreId))]
        [InverseProperty(nameof(TypeStatusOffre.TypesOffres))]
        public virtual TypeStatusOffre EstTypeOffre { get; set; } = null!;
    }
}
