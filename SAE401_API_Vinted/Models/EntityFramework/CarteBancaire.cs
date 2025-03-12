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

        [Column("cob_id")]
        public int CompteId { get; set; }

        [Column("cab_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        [Column("cab_prenom")]
        [StringLength(50)]
        public string Prenom { get; set; }

        [Column("cab_numero", TypeName = "char(16)")]
        public string Numero { get; set; }

        [Column("cab_dateexpiration", TypeName = "date")]
        public DateTime DateExpiration { get; set; }

        [Column("cab_cryptogramme", TypeName = "char(3)")]
        public string Cryptogramme { get; set; }

        [ForeignKey(nameof(CompteId))]
        [InverseProperty(nameof(CompteBancaire.CartesCompte))]
        public virtual CompteBancaire CompteIdNavigation { get; set; } = null!;
    }
}
