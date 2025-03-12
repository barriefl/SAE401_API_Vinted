using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_etatarticle_eta")]
    public class EtatArticle
    {
        [Key]
        [Column("eta_id")]
        public int EtatArticleId { get; set; }

        [Required]
        [Column("eta_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Article.EtatDeArticle))]
        public virtual ICollection<Article> EtatsDesArticles { get; set; } = new List<Article>();
    }
}
