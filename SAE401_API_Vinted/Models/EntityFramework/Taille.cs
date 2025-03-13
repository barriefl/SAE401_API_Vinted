using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_taille_tal")]
    public class Taille
    {
        [Key]
        [Column("tal_id")]
        public int TailleId { get; set; }

        [Required]
        [Column("tta_id")]
        public int TypeTailleId { get; set; }

        [Required]
        [Column("tal_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; }

        [ForeignKey(nameof(TypeTailleId))]
        [InverseProperty(nameof(TypeTaille.TaillesTypeTaille))]
        public virtual TypeTaille TypeTailleIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(TailleArticle.TailleIdNavigation))]
        public virtual ICollection<TailleArticle> ArticlesTaille { get; set; } = new List<TailleArticle>();
    }
}
