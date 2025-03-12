using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_marque_mrq")]
    public class Marque
    {
        [Key]
        [Column("mrq_id")]
        public int MarqueId { get; set; }

        [Required]
        [Column("mrq_libelle")]
        [StringLength(100)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Article.MarqueDeArticle))]
        public virtual ICollection<Article> MarquesDesArticles { get; set; } = new List<Article>();
    }
}
