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
        [StringLength(50)]
        public string Libelle { get; set; } = null!;

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

        public override bool Equals(object? obj)
        {
            return obj is Categorie categorie &&
                   CategorieId == categorie.CategorieId &&
                   Libelle == categorie.Libelle &&
                   IdParent == categorie.IdParent &&
                   EqualityComparer<Categorie>.Default.Equals(CategorieParentIdNavigation, categorie.CategorieParentIdNavigation) &&
                   EqualityComparer<ICollection<Categorie>>.Default.Equals(CategoriesParent, categorie.CategoriesParent) &&
                   EqualityComparer<ICollection<TypeTaille>>.Default.Equals(TypesTaillesCategories, categorie.TypesTaillesCategories) &&
                   EqualityComparer<ICollection<Article>>.Default.Equals(CategoriesArticles, categorie.CategoriesArticles);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CategorieId);
        }

        public Categorie()
        {
        }

        public Categorie(int categorieId, string libelle, int? idParent, Categorie categorieParentIdNavigation, ICollection<Categorie> categoriesParent, ICollection<TypeTaille> typesTaillesCategories, ICollection<Article> categoriesArticles)
        {
            CategorieId = categorieId;
            Libelle = libelle;
            IdParent = idParent;
            CategorieParentIdNavigation = categorieParentIdNavigation;
            CategoriesParent = categoriesParent;
            TypesTaillesCategories = typesTaillesCategories;
            CategoriesArticles = categoriesArticles;
        }

        public Categorie(int categorieId, string libelle, int? idParent)
        {
            CategorieId = categorieId;
            Libelle = libelle;
            IdParent = idParent;
        }
    }
}
