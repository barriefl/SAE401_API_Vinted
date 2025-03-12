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

        [Column("car_id")]
        public int CaracteristiqueId { get; set; }

        [Column("tta_id")]
        public int TypeTailleId { get; set; }

        [Column("tal_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; }

        [ForeignKey(nameof(CaracteristiqueId))]
        [InverseProperty(nameof(Caracteristique.TaillesCaracteristique))]
        public virtual Caracteristique CaracteristiqueIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(Reside.ResideVintie))]
        public virtual ICollection<Reside> VintiesResides { get; set; } = new List<Reside>();

        [InverseProperty(nameof(Article.VendeurDeArticle))]
        public virtual ICollection<Article> ArticlesDuVendeur { get; set; } = new List<Article>();

    }
}
