using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_offre_ofr")]
    public class Offre : Message
    {
        [Column("ofr_montant", TypeName = "numeric(6,2)")]
        public double OffreMontant { get; set; }

        [ForeignKey(nameof(TypeStatusOffre.TypeStatusOffreId))]
        [Required]
        [Column("ofr_idtypestatusoffre")]
        public int OffreIdTypeStatusOffre { get; set; }
    }
}
