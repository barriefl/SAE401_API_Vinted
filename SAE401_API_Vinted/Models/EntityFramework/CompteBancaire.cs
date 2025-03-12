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

        [Column("cob_iban")]
        [StringLength(34)]
        public string Iban { get; set; }

        [Column("cob_nomtitulaire")]
        [StringLength(50)]
        public string NomTitulaire { get; set; }

        [Column("cob_prenomtitulaire")]
        [StringLength(50)]
        public string PrenomTitulaire { get; set; }

        [InverseProperty("CompteIdNavigation")]
        public virtual ICollection<CarteBancaire> CartesCompte { get; set; } = new List<CarteBancaire>();

        [InverseProperty("CompteIdNavigation")]
        public virtual ICollection<Appartient> AppartientCompte { get; set; } = new List<Appartient>();
    }
}
