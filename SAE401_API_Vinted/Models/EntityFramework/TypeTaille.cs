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

        [Column("tta_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; }

        [Column("tta_codecategorie")]
        [ForeignKey(nameof(TypeTailleId))]
        public int CodeCategorie { get; set; }

        [InverseProperty(nameof(TypeTaillesCategorie))]
        public virtual TypeTaille CaracteristiqueIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(CaracteristiqueIdNavigation))]
        public virtual ICollection<TypeTaille> TypeTaillesCategorie { get; set; } = new List<TypeTaille>();

        [InverseProperty(nameof(Taille.TypeTailleIdNavigation))]
        public virtual ICollection<Taille> TaillesTypeTaille { get; set; } = new List<Taille>();

    }
}
