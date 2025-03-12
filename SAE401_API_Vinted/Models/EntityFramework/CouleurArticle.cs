using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_couleurarticle_cla")]
    [PrimaryKey(nameof(ArticleId), nameof(CouleurId))]
    public class CouleurArticle
    {
        [Key]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Key]
        [Column("cla_couleurid")]
        public int CouleurId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.CouleursArticle))]
        public virtual Article ArticleConcerne { get; set; } = null!;

        [ForeignKey(nameof(CouleurId))]
        [InverseProperty(nameof(Couleur.CouleursDesArticles))]
        public virtual Couleur CouleurConcernee { get; set; } = null!;
    }
}
