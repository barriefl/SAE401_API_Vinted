using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_articletaille_tar")]
    public class ArticleTaille
    {
        [Key]
        [Column("tar_articleid")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleId { get; set; }

        [Key]
        [Column("tar_tailleid")]
        [ForeignKey(nameof(Taille.TailleId))]
        public int TailleId { get; set; }

        [InverseProperty(nameof(Article.TaillesArticle))]
        public virtual Article ArticleIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(Taille.ArticlesTaille))]
        public virtual Taille TailleIdNavigation { get; set; }
    }
}
