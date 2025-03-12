using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_categorie_cat")]
    public class Categorie
    {
        [Key]
        [Column("cat_id")]
        public int CategorieId { get; set; }

        [Column("cat_nom")]
        [StringLength(40)]
        public string CategorieNom { get; set; }

        [Column("cat_idparent")]
        [ForeignKey(nameof(CategorieId))]
        public int IdParent { get; set; }

        [InverseProperty(nameof(CategoriesParent))]
        public virtual TypeTaille CategorieParentIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(CategorieParentIdNavigation))]
        public virtual ICollection<TypeTaille> CategoriesParent { get; set; } = new List<TypeTaille>();

        [InverseProperty(nameof(Taille.TypeTailleIdNavigation))]
        public virtual ICollection<Taille> TaillesTypeTaille { get; set; } = new List<Taille>();
    }
}
