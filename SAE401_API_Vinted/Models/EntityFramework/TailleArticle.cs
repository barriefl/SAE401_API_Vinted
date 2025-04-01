using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_taillearticle_tar")]
    [PrimaryKey(nameof(ArticleId), nameof(TailleId))]
    public class TailleArticle
    {
        [Key]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Key]
        [Column("tal_id")]
        public int TailleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.TaillesArticle))]
        public virtual Article ArticleIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(TailleId))]
        [InverseProperty(nameof(Taille.ArticlesTaille))]
        public virtual Taille TailleIdNavigation { get; set; } =null!;

        public override bool Equals(object? obj)
        {
            return obj is TailleArticle article &&
                   ArticleId == article.ArticleId &&
                   TailleId == article.TailleId &&
                   EqualityComparer<Article>.Default.Equals(ArticleIdNavigation, article.ArticleIdNavigation) &&
                   EqualityComparer<Taille>.Default.Equals(TailleIdNavigation, article.TailleIdNavigation);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ArticleId, TailleId);
        }

        public TailleArticle()
        {
        }

        public TailleArticle(int articleId, int tailleId, Article articleIdNavigation, Taille tailleIdNavigation)
        {
            ArticleId = articleId;
            TailleId = tailleId;
            ArticleIdNavigation = articleIdNavigation;
            TailleIdNavigation = tailleIdNavigation;
        }

        public TailleArticle(int articleId, int tailleId)
        {
            ArticleId = articleId;
            TailleId = tailleId;
        }
    }
}
