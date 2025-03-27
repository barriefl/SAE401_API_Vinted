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

        public override bool Equals(object? obj)
        {
            return obj is Possede possede &&
                   AdresseId == possede.AdresseId &&
                   CodeType == possede.CodeType &&
                   EqualityComparer<Adresse>.Default.Equals(APourAdresse, possede.APourAdresse) &&
                   EqualityComparer<TypeAdresse>.Default.Equals(APourType, possede.APourType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(AdresseId, CodeType);
        }

        public Possede()
        {
        }

        public Possede(int adresseId, int codeType, Adresse aPourAdresse, TypeAdresse aPourType)
        {
            AdresseId = adresseId;
            CodeType = codeType;
            APourAdresse = aPourAdresse;
            APourType = aPourType;
        }

        public Possede(int adresseId, int codeType)
        {
            AdresseId = adresseId;
            CodeType = codeType;
        }
    }
}
