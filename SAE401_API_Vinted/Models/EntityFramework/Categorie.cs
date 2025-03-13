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

        [Required]
        [Column("cat_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; }

        [Column("cat_idparent")]
        public int? IdParent { get; set; }

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(CategoriesParent))]
        public virtual Categorie CategorieParentIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(CategorieParentIdNavigation))]
        public virtual ICollection<Categorie> CategoriesParent { get; set; } = new List<Categorie>();

        [InverseProperty(nameof(TypeTaille.CategorieTypeTaille))]
        public virtual ICollection<TypeTaille> TypesTaillesCategories { get; set; } = new List<TypeTaille>();

        [InverseProperty(nameof(Article.CategorieDeArticle))]
        public virtual ICollection<Article> CategoriesArticles { get; set; } = new List<Article>();
    }
}
