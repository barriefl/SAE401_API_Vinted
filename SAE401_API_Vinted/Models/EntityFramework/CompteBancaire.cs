using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_comptebancaire_cob")]
    public class CompteBancaire
    {
        [Key]
        [Column("cob_id")]
        public int CompteId { get; set; }

        [Required]
        [Column("cob_iban", TypeName = "char(27)")]
        public string Iban { get; set; } = null!;

        [Required]
        [Column("cob_nomtitulaire")]
        [StringLength(50)]
        public string NomTitulaire { get; set; } = null!;

        [Required]
        [Column("cob_prenomtitulaire")]
        [StringLength(50)]
        public string PrenomTitulaire { get; set; } = null!;

        [InverseProperty("CompteIdNavigation")]
        public virtual ICollection<CarteBancaire> CartesCompte { get; set; } = new List<CarteBancaire>();

        [InverseProperty("CompteIdNavigation")]
        public virtual ICollection<Appartient> AppartientCompte { get; set; } = new List<Appartient>();
    }
}
