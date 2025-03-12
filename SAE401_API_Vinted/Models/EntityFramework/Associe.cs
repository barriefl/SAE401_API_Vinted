using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_associe_ass")]
    public class Associe
    {
        [Key]
        [Column("rsd_adresseid")]
        [ForeignKey(nameof(Adresse.AdresseID))]
        public int AdresseID { get; set; }

        [Key]
        [Column("rsd_vintieid")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieID { get; set; }


        [InverseProperty(nameof(Adresse.AResidents))]
        public virtual Adresse ResideA { get; set; } = null!;

        [InverseProperty(nameof(Vintie.VintiesResides))]
        public virtual Vintie ResideVintie { get; set; } = null!;
    }
}
