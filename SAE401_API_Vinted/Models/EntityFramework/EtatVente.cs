using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_etatventearticle_eva")]
    public class EtatVente
    {
        [Key]
        [Column("eva_id")]
        public int EtatVenteArticleId { get; set; }

        [Required]
        [Column("eva_libelle")]
        [StringLength(20)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Article.EtatVenteDeArticle))]
        public virtual ICollection<Article> EtatsVenteDesArticles { get; set; } = new List<Article>();
    }
}
