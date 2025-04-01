using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_matierearticle_mar")]
    [PrimaryKey(nameof(MatiereId), nameof(ArticleId))]

    public class MatiereArticle
    {
        [Key]
        [Column("mat_id")]
        public int MatiereId { get; set; }

        [Key]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(MatiereId))]
        [InverseProperty(nameof(Matiere.MatieresDesArticles))]
        public virtual Matiere MatiereDeArticle { get; set; } = null!;

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.ArticlesMatieres))]
        public virtual Article ArticleMatiere { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            return obj is MatiereArticle article &&
                   MatiereId == article.MatiereId &&
                   ArticleId == article.ArticleId &&
                   EqualityComparer<Matiere>.Default.Equals(MatiereDeArticle, article.MatiereDeArticle) &&
                   EqualityComparer<Article>.Default.Equals(ArticleMatiere, article.ArticleMatiere);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(MatiereId, ArticleId);
        }

        public MatiereArticle()
        {
        }

        public MatiereArticle(int matiereId, int articleId, Matiere matiereDeArticle, Article articleMatiere)
        {
            MatiereId = matiereId;
            ArticleId = articleId;
            MatiereDeArticle = matiereDeArticle;
            ArticleMatiere = articleMatiere;
        }

        public MatiereArticle(int matiereId, int articleId)
        {
            MatiereId = matiereId;
            ArticleId = articleId;
        }
    }
}
