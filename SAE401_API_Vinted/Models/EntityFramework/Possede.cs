using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_possede_psd")]
    public class Possede
    {
        [Key]
        [Column("psd_codetype")]
        [ForeignKey(nameof(TypeAdresse.Code))]
        public int Code { get; set; }

        [Key]
        [Column("psd_adresseid")]
        [ForeignKey(nameof(Adresse.AdresseID))]
        public int AdresseId { get; set; }


        [InverseProperty(nameof(Adresse.PossedesAdresse))]
        public virtual Adresse APourAdresse { get; set; } = null!;

        [InverseProperty(nameof(TypeAdresse.PossedesType))]
        public virtual TypeAdresse APourType { get; set; } = null!;
    }
}
