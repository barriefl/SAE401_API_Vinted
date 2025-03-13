using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_possede_psd")]
    [PrimaryKey(nameof(CodeType), nameof(AdresseId))]
    public class Possede
    {
        [Key]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Key]
        [Column("tad_id")]
        public int CodeType { get; set; }

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.PossedesAdresse))]
        public virtual Adresse APourAdresse { get; set; } = null!;

        [ForeignKey(nameof(CodeType))]
        [InverseProperty(nameof(TypeAdresse.PossedesType))]
        public virtual TypeAdresse APourType { get; set; } = null!;
    }
}
