using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_couleurarticle_cla")]
    public class CouleurArticle
    {
        [Key]
        [Column("cla_articleid")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleId { get; set; }

        [Key]
        [Column("cla_couleurid")]
        [ForeignKey(nameof(Couleur.CouleurId))]
        public int CouleurId { get; set; }

        [InverseProperty(nameof(Article.CouleursArticle))]
        public virtual Article ArticleConcerne { get; set; } = null!;

        [InverseProperty(nameof(Couleur.CouleursDesArticles))]
        public virtual Couleur CouleurConcernee { get; set; } = null!;
    }
}
