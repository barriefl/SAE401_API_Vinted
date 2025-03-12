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

        [ForeignKey(nameof(TypeTailleId))]
        [Column("tta_codecategorie")]
        public int TypeTailleParentId { get; set; }

        [InverseProperty(nameof(TypeTaillesEnfants))]
        public virtual TypeTaille TypeTailleParent { get; set; } = null!;

        [InverseProperty(nameof(TypeTailleParent))]
        public virtual ICollection<TypeTaille> TypeTaillesEnfants { get; set; } = new List<TypeTaille>();

        [InverseProperty(nameof(Taille.TypeTailleIdNavigation))]
        public virtual ICollection<Taille> TaillesTypeTaille { get; set; } = new List<Taille>();

    }
}
