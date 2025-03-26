using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_comptebancaire_cob")]
    public class CompteBancaire
    {
        public CompteBancaire()
        {
        }

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

        public override bool Equals(object? obj)
        {
            return obj is CompteBancaire bancaire &&
                   CompteId == bancaire.CompteId &&
                   Iban == bancaire.Iban &&
                   NomTitulaire == bancaire.NomTitulaire &&
                   PrenomTitulaire == bancaire.PrenomTitulaire;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompteId);
        }

        public CompteBancaire(int compteId, string iban, string nomTitulaire, string prenomTitulaire, ICollection<CarteBancaire> cartesCompte, ICollection<Appartient> appartientCompte)
        {
            CompteId = compteId;
            Iban = iban;
            NomTitulaire = nomTitulaire;
            PrenomTitulaire = prenomTitulaire;
            CartesCompte = cartesCompte;
            AppartientCompte = appartientCompte;
        }

        public CompteBancaire(int compteId, string iban, string nomTitulaire, string prenomTitulaire)
        {
            CompteId = compteId;
            Iban = iban;
            NomTitulaire = nomTitulaire;
            PrenomTitulaire = prenomTitulaire;
        }
    }
}
