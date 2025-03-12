using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_vinties_vnt")]
    public class Vintie
    {
        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Column("vnt_pseudo")]
        [StringLength(50)]
        public string Pseudo { get; set; }

        [Column("tco_codetypecompte")]
        public int CodeTypeCompte { get; set; }

        [Column("vnt_nom")]
        public string Nom { get; set; }

        [Column("vnt_prenom")]
        public string Prenom { get; set; }

        [Column("vnt_civilite", TypeName="Char(1)")]
        public string Civilite { get; set; }

        [Column("vnt_mail")]
        [EmailAddress(ErrorMessage = "Format de mail incorrect")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "La longueru d'un mail se situe entre 6 et 150 caractères.")]
        public string Mail { get; set; }

        [Column("vnt_pwd")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un carac spécial (#?!@$%^&*-)")]
        public string Pwd { get; set; }

        [Column("vnt_telephone", TypeName ="Char(14)")]
        [RegularExpression(@"[0-9]{10}$", ErrorMessage = "Le numéro de tel doit contenir 10 chiffres")]
        public string Telephone { get; set; }

        [Column("vnt_datenaissance", TypeName ="date")]
        public DateTime? DateNaissance { get; set; }

        [Column("vnt_urlphoto")]
        [StringLength(500)]
        public string? URLPhoto { get; set; }

        [Column("vnt_dateinscription", TypeName ="date")]
        public DateTime DateInscription { get; set; }

        [Column("vnt_montantcompte", TypeName = "numeric")]
        public double MontantCompte { get; set; }

        [Column("vnt_statut")]
        [StringLength(15)]
        public string Statut { get; set; }

        [Column("vnt_consentement")]
        public bool Consentement { get; set; }

        [Column("vnt_siret")]
        [StringLength (18)]
        public string? Siret { get; set; }

        [ForeignKey(nameof(CodeTypeCompte))]
        [InverseProperty(nameof(TypeCompte.VintiesType))]
        public virtual TypeCompte VintieCodeNavigation { get; set; } = null!;

        [InverseProperty(nameof(Reside.ResideVintie))]
        public virtual ICollection<Reside> VintiesResides { get; set; } = new List<Reside>();

        [InverseProperty(nameof(Article.VendeurDeArticle))]
        public virtual ICollection<Article> ArticlesDuVendeur { get; set; } = new List<Article>();

        [InverseProperty(nameof(Appartient.VintieIdNavigation))]
        public virtual ICollection<Appartient> AppartienentVintie { get; set; } = new List<Appartient>();

        [InverseProperty(nameof(Avis.ApourVendeur))]
        public virtual ICollection<Avis> ADesAvis { get; set; } = new List<Avis>();

    }
}
