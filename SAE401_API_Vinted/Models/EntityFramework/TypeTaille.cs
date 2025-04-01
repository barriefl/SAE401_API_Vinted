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

        [Column("cat_id")]
        public int CategorieId { get; set; }

        [Required]
        [Column("tta_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [ForeignKey(nameof(CategorieId))]
        [InverseProperty(nameof(Categorie.TypesTaillesCategories))]
        public virtual Categorie CategorieTypeTaille { get; set; } = null!;

        [InverseProperty(nameof(Taille.TypeTailleIdNavigation))]
        public virtual ICollection<Taille> TaillesTypeTaille { get; set; } = new List<Taille>();

        public override bool Equals(object? obj)
        {
            return obj is TypeTaille taille &&
                   TypeTailleId == taille.TypeTailleId &&
                   CategorieId == taille.CategorieId &&
                   Libelle == taille.Libelle &&
                   EqualityComparer<Categorie>.Default.Equals(CategorieTypeTaille, taille.CategorieTypeTaille) &&
                   EqualityComparer<ICollection<Taille>>.Default.Equals(TaillesTypeTaille, taille.TaillesTypeTaille);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TypeTailleId);
        }

        public TypeTaille()
        {
        }

        public TypeTaille(int typeTailleId, int categorieId, string libelle, Categorie categorieTypeTaille, ICollection<Taille> taillesTypeTaille)
        {
            TypeTailleId = typeTailleId;
            CategorieId = categorieId;
            Libelle = libelle;
            CategorieTypeTaille = categorieTypeTaille;
            TaillesTypeTaille = taillesTypeTaille;
        }

        public TypeTaille(int typeTailleId, int categorieId, string libelle)
        {
            TypeTailleId = typeTailleId;
            CategorieId = categorieId;
            Libelle = libelle;
        }
    }
}
