using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typetaille_tta")]
    public class TypeTaille
    {
        [Key]
        [Column("tta_id")]
        public int TypeTailleId { get; set; }

        [Required]
        [Column("tta_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [Column("cat_id")]
        public int CategorieId { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.TypesTaillesCategories))]
        public virtual Categorie CategorieTypeTaille { get; set; } = null!;

        [InverseProperty(nameof(Taille.TypeTailleIdNavigation))]
        public virtual ICollection<Taille> TaillesTypeTaille { get; set; } = new List<Taille>();

    }
}
