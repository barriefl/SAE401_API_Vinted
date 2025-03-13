using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_matierearticle_mar")]
    public class MatiereArticle
    {
        [Key]
        [Column("mat_id")]
        public int MatiereId { get; set; }

        [Required]
        [Column("art_id")]
        public int ArticleId { get; set; }


        [ForeignKey(nameof(MatiereId))]
        [InverseProperty(nameof(Matiere.MatieresDesArticles))]
        public virtual Matiere MatiereDeArticle { get; set; } = null!;

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.ArticlesMatieres))]
        public virtual Article ArticleMatiere { get; set; } = null!;

    }
}
