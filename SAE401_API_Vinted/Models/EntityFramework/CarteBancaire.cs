using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_cartebancaire_cab")]
    public class CarteBancaire
    {
        [Key]
        [Column("cab_id")]
        public int CarteId { get; set; }

        [Required]
        [Column("cob_id")]
        public int CompteId { get; set; }

        [Required]
        [Column("cab_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Required]
        [Column("cab_prenom")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Required]
        [Column("cab_numero", TypeName = "char(16)")]
        public string Numero { get; set; }

        [Required]
        [Column("cab_dateexpiration", TypeName = "date")]
        public DateTime DateExpiration { get; set; }

        [ForeignKey(nameof(CompteId))]
        [InverseProperty(nameof(CompteBancaire.CartesCompte))]
        public virtual CompteBancaire CompteIdNavigation { get; set; } = null!;
    }
}
