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

        [Required]
        [Column("vnt_pseudo")]
        [StringLength(50)]
        public string Pseudo { get; set; } = null!;

        [Required]
        [Column("tyc_id")]
        public int TypeCompteId { get; set; }

        [Required]
        [Column("vnt_nom")]
        public string Nom { get; set; } = null!;

        [Required]
        [Column("vnt_prenom")]
        public string Prenom { get; set; } = null!;

        [Required]
        [Column("vnt_civilite", TypeName = "Char(1)")]
        public string Civilite { get; set; } = null!;

        [Required]
        [Column("vnt_mail")]
        [EmailAddress(ErrorMessage = "Format de mail incorrect")]
        [StringLength(150, MinimumLength = 6, ErrorMessage = "La longueru d'un mail se situe entre 6 et 150 caractères.")]
        public string Mail { get; set; } = null!;

        [Required]
        [Column("vnt_pwd")]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$", ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un carac spécial (#?!@$%^&*-)")]
        public string Pwd { get; set; } = null!;

        [Required]
        [Column("vnt_telephone", TypeName = "Char(14)")]
        [RegularExpression(@"[0-9]{10}$", ErrorMessage = "Le numéro de tel doit contenir 10 chiffres")]
        public string Telephone { get; set; } = null!;

        [Required]
        [Column("vnt_datenaissance", TypeName ="date")]
        public DateTime? DateNaissance { get; set; }

        [Required]
        [Column("vnt_urlphoto")]
        [StringLength(500)]
        public string? URLPhoto { get; set; }

        [Required]
        [Column("vnt_dateinscription", TypeName ="date")]
        public DateTime DateInscription { get; set; }

        [Required]
        [Column("vnt_montantcompte", TypeName = "numeric")]
        public double MontantCompte { get; set; }

        [Required]
        [Column("vnt_datederniereconnexion", TypeName = "date")]
        public DateTime DateDerniereConnexion { get; set; }

        [Required]
        [Column("vnt_consentement")]
        public bool Consentement { get; set; }

        [Column("vnt_siret")]
        [StringLength(18)]
        public string? Siret { get; set; }

        [ForeignKey(nameof(TypeCompteId))]
        [InverseProperty(nameof(TypeCompte.VintiesType))]
        public virtual TypeCompte VintieCodeNavigation { get; set; } = null!;

        [InverseProperty(nameof(Reside.ResideVintie))]
        public virtual ICollection<Reside> VintiesResides { get; set; } = new List<Reside>();

        [InverseProperty(nameof(Article.VendeurDeArticle))]
        public virtual ICollection<Article> ArticlesDuVendeur { get; set; } = new List<Article>();

        [InverseProperty(nameof(Appartient.VintieIdNavigation))]
        public virtual ICollection<Appartient> AppartienentVintie { get; set; } = new List<Appartient>();

        [InverseProperty(nameof(Avis.APourVendeur))]
        public virtual ICollection<Avis> ADesAvisVendeur { get; set; } = new List<Avis>();

        [InverseProperty(nameof(Avis.APourAcheteur))]
        public virtual ICollection<Avis> ADesAvisAcheteur { get; set; } = new List<Avis>();

        [InverseProperty(nameof(Preference.VintieIdNavigation))]
        public virtual ICollection<Preference> PreferencesVintie { get; set; } = new List<Preference>();

        [InverseProperty(nameof(Signalement.VintieSignalant))]
        public virtual ICollection<Signalement> SignalementsDeArticle { get; set; } = new List<Signalement>();

        [InverseProperty(nameof(Favoris.FavorisVintie))]
        public virtual ICollection<Favoris> FavorisDeVintie { get; set; } = new List<Favoris>();

        [InverseProperty(nameof(PointRelaisFavoris.VintiePointRelais))]
        public virtual ICollection<PointRelaisFavoris> PointRelaisFavorisVintie { get; set; } = new List<PointRelaisFavoris>();

        [InverseProperty(nameof(Commande.VintieCommande))]
        public virtual ICollection<Commande> CommandesVinties { get; set; } = new List<Commande>();

        [InverseProperty(nameof(Conversation.AcheteurIdNavigation))]
        public virtual ICollection<Conversation> ConversationsAcheteur { get; set; } = new List<Conversation>();

        [InverseProperty(nameof(Conversation.VendeurIdNavigation))]
        public virtual ICollection<Conversation> ConversationsVendeur { get; set; } = new List<Conversation>();


    }
}
