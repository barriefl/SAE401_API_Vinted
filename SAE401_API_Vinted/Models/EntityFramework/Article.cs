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

        [ForeignKey(nameof(EtatArticle.EtatArticleId))]
        [Column("art_marqueid")]
        public int EtatArticleId { get; set; }

        [ForeignKey(nameof(Vintie.VintieId))]
        [Column("art_vendeurid")]
        public int VendeurId { get; set; }

        [ForeignKey(nameof(Marque.MarqueId))]
        [Column("art_marqueid")]
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

        [InverseProperty(nameof(Conversation.ConversationIdArticle))]
        public virtual ICollection<Conversation> ListeConversation { get; set; } = new List<Conversation>();

        [InverseProperty(nameof(EtatArticle.EtatsDesArticles))]
        public virtual EtatArticle EtatDeArticle { get; set; } = null!;

        [InverseProperty(nameof(Vintie.ArticlesDuVendeur))]
        public virtual Vintie VendeurDeArticle { get; set; } = null!;

        [InverseProperty(nameof(Marque.MarquesDesArticles))]
        public virtual Marque MarqueDeArticle { get; set; } = null!;
    }
}
