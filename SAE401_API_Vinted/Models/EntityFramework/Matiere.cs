using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_matiere_mat")]
    public class Matiere
    {
        [Key]
        [Column("mat_id")]
        public int MatiereId { get; set; }

        [Column("mat_articleid")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleId { get; set; }

        [Required]
        [Column("mat_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Article.MatiereDeArticle))]
        public virtual ICollection<Article> MatieresDesArticles { get; set; } = new List<Article>();
    }
}
