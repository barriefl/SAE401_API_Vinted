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

        public override bool Equals(object? obj)
        {
            return obj is TypeAdresse adresse &&
                   TypeAdresseId == adresse.TypeAdresseId &&
                   Libelle == adresse.Libelle &&
                   EqualityComparer<ICollection<Possede>>.Default.Equals(PossedesType, adresse.PossedesType);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(TypeAdresseId);
        }

        public TypeAdresse()
        {
        }

        public TypeAdresse(int typeAdresseId, string libelle, ICollection<Possede> possedesType)
        {
            TypeAdresseId = typeAdresseId;
            Libelle = libelle;
            PossedesType = possedesType;
        }

        public TypeAdresse(int typeAdresseId, string libelle)
        {
            TypeAdresseId = typeAdresseId;
            Libelle = libelle;
        }
    }
}
