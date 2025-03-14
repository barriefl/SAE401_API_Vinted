using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeadresse_tad")]

    public class TypeAdresse
    {
        [Key]
        [Column("tad_id")]
        public int TypeAdresseId { get; set; }

        [Required]
        [Column("tad_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Possede.APourType))]
        public virtual ICollection<Possede> PossedesType { get; set; } = new List<Possede>();
    }
}
