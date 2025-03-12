using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_article_art")]
    public class Article
    {
        [Key]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Column("cat_id")]
        public int CategorieId { get; set; }

        [Column("eta_id")]
        public int EtatArticleId { get; set; }

        [Column("vnt_vendeurid")]
        public int VendeurId { get; set; }

        [Column("mrq_id")]
        public int MarqueId { get; set; }

        [Column("mat_id")]
        public int MatiereId { get; set; }

        [Column("eva_id")]
        public int EtatVenteArticleId { get; set; }

        [Required]
        [Column("art_titre")]
        [StringLength(100)]
        public string Titre { get; set; } = null!;

        [Required]
        [Column("art_description")]
        [StringLength(1000)]
        public string Description { get; set; } = null!;

        [Required]
        [Column("art_prixht")]
        public int PrixHT { get; set; }

        [Required]
        [Column("art_dateajout", TypeName = "date")]
        public DateTime DateAjout { get; set; }

        [Required]
        [Column("art_compteurlike")]
        public int CompteurLike { get; set; }

        [ForeignKey(nameof(EtatArticleId))]
        [InverseProperty(nameof(EtatArticle.EtatsDesArticles))]
        public virtual EtatArticle EtatDeArticle { get; set; } = null!;

        [ForeignKey(nameof(VendeurId))]
        [InverseProperty(nameof(Vintie.ArticlesDuVendeur))]
        public virtual Vintie VendeurDeArticle { get; set; } = null!;

        [InverseProperty(nameof(Conversation.ConversationIdArticle))]
        public virtual ICollection<Conversation> ListeConversation { get; set; } = new List<Conversation>();

        [ForeignKey(nameof(MarqueId))]
        [InverseProperty(nameof(Marque.MarquesDesArticles))]
        public virtual Marque MarqueDeArticle { get; set; } = null!;

        [ForeignKey(nameof(MatiereId))]
        [InverseProperty(nameof(Matiere.MatieresDesArticles))]
        public virtual Matiere MatiereDeArticle { get; set; } = null!;

        [ForeignKey(nameof(EtatVenteArticleId))]
        [InverseProperty(nameof(EtatVenteArticle.EtatsVenteDesArticles))]
        public virtual EtatVenteArticle EtatVenteDeArticle { get; set; } = null!;

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.CategoriesArticles))]
        public virtual Categorie CategorieDeArticle { get; set; } = null!;

        [InverseProperty(nameof(Image.ArticleDeImage))]
        public virtual ICollection<Image> ImagesDeArticle { get; set; } = new List<Image>();

        [InverseProperty(nameof(Signalement.ArticleDuSignalement))]
        public virtual ICollection<Signalement> SignalementsDeArticle { get; set; } = new List<Signalement>();

        [InverseProperty(nameof(Favoris.EstFavoris))]
        public virtual ICollection<Favoris> FavorisArticle { get; set; } = new List<Favoris>();

        [InverseProperty(nameof(TailleArticle.ArticleIdNavigation))]
        public virtual ICollection<TailleArticle> TaillesArticle { get; set; } = new List<TailleArticle>();

        [InverseProperty(nameof(CouleurArticle.ArticleConcerne))]
        public virtual ICollection<CouleurArticle> CouleursArticle { get; set; } = new List<CouleurArticle>();
        

        [InverseProperty(nameof(Commande.ArticleCommande))]
        public virtual ICollection<Commande> CommandesArticles { get; set; } = new List<Commande>();

        [InverseProperty(nameof(Conversation.AcheteurIdNavigation))]
        public virtual ICollection<Conversation> ConversationsArticle { get; set; } = new List<Conversation>();
    }
}
