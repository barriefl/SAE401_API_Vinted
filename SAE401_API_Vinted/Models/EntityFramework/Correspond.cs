using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_correspond_crd")]
    public class Correspond
    {
        [Key]
        [Column("crd_articleid")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleId { get; set; }

        [Key]
        [Column("crd_categorieid")]
        [ForeignKey(nameof(Categorie.CategorieId))]
        public int CategorieId { get; set; }


        [InverseProperty(nameof(Adresse.AResidents))]
        public virtual Adresse ResideA { get; set; } = null!;

        [InverseProperty(nameof(Vintie.VintiesResides))]
        public virtual Vintie ResideVintie { get; set; } = null!;
    }
}
