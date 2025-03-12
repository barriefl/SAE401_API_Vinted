using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_couleur_clr")]
    public class Couleur
    {
        [Key]
        [Column("clr_id")]
        public int CouleurId { get; set; }

        [Required]
        [Column("clr_libelle")]
        [StringLength(25)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(CouleurArticle.CouleurConcernee))]
        public virtual ICollection<CouleurArticle> CouleursDesArticles { get; set; } = new List<CouleurArticle>();
    }
}
