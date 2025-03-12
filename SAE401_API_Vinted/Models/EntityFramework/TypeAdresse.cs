using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeadresse_tad")]

    public class TypeAdresse
    {
        [Key]
        [Column("tad_id")]
        public int Code { get; set; }

        [Required]
        [Column("tad_libelle")]
        public string LibelleType { get; set; } = null!;


        [InverseProperty(nameof(Possede.APourType))]
        public virtual ICollection<Possede> PossedesType { get; set; } = new List<Possede>();
    }
}
