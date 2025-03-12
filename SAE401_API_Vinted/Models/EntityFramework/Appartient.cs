using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_appartient_app")]
    public class Appartient
    {
        [Key]
        [Column("cob_id")]
        public int CompteId { get; set; }

        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [ForeignKey(nameof(CompteId))]
        [InverseProperty(nameof(CompteBancaire.AppartientCompte))]
        public virtual CompteBancaire CompteIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(CompteBancaire.CartesCompte))]
        public virtual CompteBancaire VintieIdNavigation { get; set; } = null!;
    }
}
