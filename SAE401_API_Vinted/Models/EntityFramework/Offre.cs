using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_offre_ofr")]
    public class Offre : Message
    {
        [Required]
        [Column("tso_id")]
        public int TypeStatusOffreId { get; set; }

        [Required]
        [Range(0, 9999.99)]
        [Column("ofr_montant", TypeName = "numeric(6,2)")]
        public double Montant { get; set; }

        [ForeignKey(nameof(TypeStatusOffreId))]
        [InverseProperty(nameof(StatusOffre.StatusOffres))]
        public virtual StatusOffre EstStatusOffre { get; set; } = null!;
    }
}
