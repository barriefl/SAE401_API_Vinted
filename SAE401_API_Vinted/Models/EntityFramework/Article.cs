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

        [Required]
        [Column("cat_id")]
        public int CategorieId { get; set; }

        [Required]
        [Column("vnt_vendeurid")]
        public int VendeurId { get; set; }

        [Required]
        [Column("eva_id")]
        public int EtatVenteArticleId { get; set; }

        [Required]
        [Column("eta_id")]
        public int EtatArticleId { get; set; }

        [Required]
        [Column("mrq_id")]
        public int MarqueId { get; set; }

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

        [ForeignKey(nameof(MarqueId))]
        [InverseProperty(nameof(Marque.MarquesDesArticles))]
        public virtual Marque MarqueDeArticle { get; set; } = null!;

        

        [InverseProperty(nameof(MatiereArticle.ArticleMatiere))]
        public virtual ICollection<MatiereArticle> ArticlesMatieres { get; set; } = new List<MatiereArticle>();

        [ForeignKey(nameof(EtatVenteArticleId))]
        [InverseProperty(nameof(EtatVente.EtatsVenteDesArticles))]
        public virtual EtatVente EtatVenteDeArticle { get; set; } = null!;

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

        [InverseProperty(nameof(Conversation.ArticleIdNavigation))]
        public virtual ICollection<Conversation> ConversationsArticle { get; set; } = new List<Conversation>();

        [InverseProperty(nameof(Retour.ArticleRetourne))]
        public virtual ICollection<Retour> RetourDesArticles { get; set; } = new List<Retour>();

        public Article()
        {
        }

        public Article(int articleId, int categorieId, int vendeurId, int etatVenteArticleId, int etatArticleId, int marqueId, string titre, string description, int prixHT, DateTime dateAjout, int compteurLike, EtatArticle etatDeArticle, Vintie vendeurDeArticle, Marque marqueDeArticle, ICollection<MatiereArticle> articlesMatieres, EtatVente etatVenteDeArticle, Categorie categorieDeArticle, ICollection<Image> imagesDeArticle, ICollection<Signalement> signalementsDeArticle, ICollection<Favoris> favorisArticle, ICollection<TailleArticle> taillesArticle, ICollection<CouleurArticle> couleursArticle, ICollection<Commande> commandesArticles, ICollection<Conversation> conversationsArticle, ICollection<Retour> retourDesArticles)
        {
            ArticleId = articleId;
            CategorieId = categorieId;
            VendeurId = vendeurId;
            EtatVenteArticleId = etatVenteArticleId;
            EtatArticleId = etatArticleId;
            MarqueId = marqueId;
            Titre = titre;
            Description = description;
            PrixHT = prixHT;
            DateAjout = dateAjout;
            CompteurLike = compteurLike;
            EtatDeArticle = etatDeArticle;
            VendeurDeArticle = vendeurDeArticle;
            MarqueDeArticle = marqueDeArticle;
            ArticlesMatieres = articlesMatieres;
            EtatVenteDeArticle = etatVenteDeArticle;
            CategorieDeArticle = categorieDeArticle;
            ImagesDeArticle = imagesDeArticle;
            SignalementsDeArticle = signalementsDeArticle;
            FavorisArticle = favorisArticle;
            TaillesArticle = taillesArticle;
            CouleursArticle = couleursArticle;
            CommandesArticles = commandesArticles;
            ConversationsArticle = conversationsArticle;
            RetourDesArticles = retourDesArticles;
        }

        public Article(int categorieId, int vendeurId, int etatVenteArticleId, int etatArticleId, int marqueId, string titre, string description, int prixHT, DateTime dateAjout, int compteurLike)
        {
            CategorieId = categorieId;
            VendeurId = vendeurId;
            EtatVenteArticleId = etatVenteArticleId;
            EtatArticleId = etatArticleId;
            MarqueId = marqueId;
            Titre = titre;
            Description = description;
            PrixHT = prixHT;
            DateAjout = dateAjout;
            CompteurLike = compteurLike;
        }

        public override bool Equals(object? obj)
        {
            return obj is Article article &&
                   ArticleId == article.ArticleId &&
                   CategorieId == article.CategorieId &&
                   VendeurId == article.VendeurId &&
                   EtatVenteArticleId == article.EtatVenteArticleId &&
                   EtatArticleId == article.EtatArticleId &&
                   MarqueId == article.MarqueId &&
                   Titre == article.Titre &&
                   Description == article.Description &&
                   PrixHT == article.PrixHT &&
                   DateAjout == article.DateAjout &&
                   CompteurLike == article.CompteurLike &&
                   EqualityComparer<EtatArticle>.Default.Equals(EtatDeArticle, article.EtatDeArticle) &&
                   EqualityComparer<Vintie>.Default.Equals(VendeurDeArticle, article.VendeurDeArticle) &&
                   EqualityComparer<Marque>.Default.Equals(MarqueDeArticle, article.MarqueDeArticle) &&
                   EqualityComparer<ICollection<MatiereArticle>>.Default.Equals(ArticlesMatieres, article.ArticlesMatieres) &&
                   EqualityComparer<EtatVente>.Default.Equals(EtatVenteDeArticle, article.EtatVenteDeArticle) &&
                   EqualityComparer<Categorie>.Default.Equals(CategorieDeArticle, article.CategorieDeArticle) &&
                   EqualityComparer<ICollection<Image>>.Default.Equals(ImagesDeArticle, article.ImagesDeArticle) &&
                   EqualityComparer<ICollection<Signalement>>.Default.Equals(SignalementsDeArticle, article.SignalementsDeArticle) &&
                   EqualityComparer<ICollection<Favoris>>.Default.Equals(FavorisArticle, article.FavorisArticle) &&
                   EqualityComparer<ICollection<TailleArticle>>.Default.Equals(TaillesArticle, article.TaillesArticle) &&
                   EqualityComparer<ICollection<CouleurArticle>>.Default.Equals(CouleursArticle, article.CouleursArticle) &&
                   EqualityComparer<ICollection<Commande>>.Default.Equals(CommandesArticles, article.CommandesArticles) &&
                   EqualityComparer<ICollection<Conversation>>.Default.Equals(ConversationsArticle, article.ConversationsArticle) &&
                   EqualityComparer<ICollection<Retour>>.Default.Equals(RetourDesArticles, article.RetourDesArticles);
        }
    }
}
