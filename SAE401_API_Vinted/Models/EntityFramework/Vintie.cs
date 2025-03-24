using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_vintie_vnt")]
    [Index(nameof(Mail), Name = "uq_vnt_mail", IsUnique = true)]
    public class Vintie
    {
        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Required]
        [Column("tyc_id")]
        public int TypeCompteId { get; set; }

        [Required]
        [Column("vnt_pseudo")]
        [StringLength(50)]
        public string Pseudo { get; set; } = null!;

        [Required]
        [Column("vnt_nom")]
        [StringLength(50)]
        public string Nom { get; set; } = null!;

        [Required]
        [Column("vnt_prenom")]
        [StringLength(50)]
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
        [StringLength(64)]
        [PasswordPropertyText]
        [RegularExpression(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,20}$", ErrorMessage = "Le mot de passe doit contenir au moins une majuscule, une minuscule, un chiffre et un caractère spécial (#?!@$%^&*-) et est composé de 8 à 12 caractères.")]
        
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

        [InverseProperty(nameof(Retour.VintieRetour))]
        public virtual ICollection<Retour> RetourDesVintie { get; set; } = new List<Retour>();

        public override bool Equals(object? obj)
        {
            return obj is Vintie vintie &&
                   VintieId == vintie.VintieId &&
                   TypeCompteId == vintie.TypeCompteId &&
                   Pseudo == vintie.Pseudo &&
                   Nom == vintie.Nom &&
                   Prenom == vintie.Prenom &&
                   Civilite == vintie.Civilite &&
                   Mail == vintie.Mail &&
                   Pwd == vintie.Pwd &&
                   Telephone == vintie.Telephone &&
                   DateNaissance == vintie.DateNaissance &&
                   URLPhoto == vintie.URLPhoto &&
                   DateInscription == vintie.DateInscription &&
                   MontantCompte == vintie.MontantCompte &&
                   DateDerniereConnexion == vintie.DateDerniereConnexion &&
                   Consentement == vintie.Consentement &&
                   Siret == vintie.Siret &&
                   EqualityComparer<TypeCompte>.Default.Equals(VintieCodeNavigation, vintie.VintieCodeNavigation) &&
                   EqualityComparer<ICollection<Reside>>.Default.Equals(VintiesResides, vintie.VintiesResides) &&
                   EqualityComparer<ICollection<Article>>.Default.Equals(ArticlesDuVendeur, vintie.ArticlesDuVendeur) &&
                   EqualityComparer<ICollection<Appartient>>.Default.Equals(AppartienentVintie, vintie.AppartienentVintie) &&
                   EqualityComparer<ICollection<Avis>>.Default.Equals(ADesAvisVendeur, vintie.ADesAvisVendeur) &&
                   EqualityComparer<ICollection<Avis>>.Default.Equals(ADesAvisAcheteur, vintie.ADesAvisAcheteur) &&
                   EqualityComparer<ICollection<Preference>>.Default.Equals(PreferencesVintie, vintie.PreferencesVintie) &&
                   EqualityComparer<ICollection<Signalement>>.Default.Equals(SignalementsDeArticle, vintie.SignalementsDeArticle) &&
                   EqualityComparer<ICollection<Favoris>>.Default.Equals(FavorisDeVintie, vintie.FavorisDeVintie) &&
                   EqualityComparer<ICollection<PointRelaisFavoris>>.Default.Equals(PointRelaisFavorisVintie, vintie.PointRelaisFavorisVintie) &&
                   EqualityComparer<ICollection<Commande>>.Default.Equals(CommandesVinties, vintie.CommandesVinties) &&
                   EqualityComparer<ICollection<Conversation>>.Default.Equals(ConversationsAcheteur, vintie.ConversationsAcheteur) &&
                   EqualityComparer<ICollection<Retour>>.Default.Equals(RetourDesVintie, vintie.RetourDesVintie);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VintieId);
        }

        public Vintie()
        {
        }

        public Vintie(int vintieId, int typeCompteId, string pseudo, string nom, string prenom, string civilite, string mail, string pwd, string telephone, DateTime? dateNaissance, string? uRLPhoto, DateTime dateInscription, double montantCompte, DateTime dateDerniereConnexion, bool consentement, string? siret, TypeCompte vintieCodeNavigation, ICollection<Reside> vintiesResides, ICollection<Article> articlesDuVendeur, ICollection<Appartient> appartienentVintie, ICollection<Avis> aDesAvisVendeur, ICollection<Avis> aDesAvisAcheteur, ICollection<Preference> preferencesVintie, ICollection<Signalement> signalementsDeArticle, ICollection<Favoris> favorisDeVintie, ICollection<PointRelaisFavoris> pointRelaisFavorisVintie, ICollection<Commande> commandesVinties, ICollection<Conversation> conversationsAcheteur, ICollection<Retour> retourDesVintie)
        {
            VintieId = vintieId;
            TypeCompteId = typeCompteId;
            Pseudo = pseudo;
            Nom = nom;
            Prenom = prenom;
            Civilite = civilite;
            Mail = mail;
            Pwd = pwd;
            Telephone = telephone;
            DateNaissance = dateNaissance;
            URLPhoto = uRLPhoto;
            DateInscription = dateInscription;
            MontantCompte = montantCompte;
            DateDerniereConnexion = dateDerniereConnexion;
            Consentement = consentement;
            Siret = siret;
            VintieCodeNavigation = vintieCodeNavigation;
            VintiesResides = vintiesResides;
            ArticlesDuVendeur = articlesDuVendeur;
            AppartienentVintie = appartienentVintie;
            ADesAvisVendeur = aDesAvisVendeur;
            ADesAvisAcheteur = aDesAvisAcheteur;
            PreferencesVintie = preferencesVintie;
            SignalementsDeArticle = signalementsDeArticle;
            FavorisDeVintie = favorisDeVintie;
            PointRelaisFavorisVintie = pointRelaisFavorisVintie;
            CommandesVinties = commandesVinties;
            ConversationsAcheteur = conversationsAcheteur;
            RetourDesVintie = retourDesVintie;
        }

        public Vintie(int vintieId, int typeCompteId, string pseudo, string nom, string prenom, string civilite, string mail, string pwd, string telephone, DateTime? dateNaissance, string? uRLPhoto, DateTime dateInscription, double montantCompte, DateTime dateDerniereConnexion, bool consentement, string? siret)
        {
            VintieId = vintieId;
            TypeCompteId = typeCompteId;
            Pseudo = pseudo;
            Nom = nom;
            Prenom = prenom;
            Civilite = civilite;
            Mail = mail;
            Pwd = pwd;
            Telephone = telephone;
            DateNaissance = dateNaissance;
            URLPhoto = uRLPhoto;
            DateInscription = dateInscription;
            MontantCompte = montantCompte;
            DateDerniereConnexion = dateDerniereConnexion;
            Consentement = consentement;
            Siret = siret;
        }
    }
}