using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_associe_ass")]
    [PrimaryKey(nameof(AdresseID), nameof(VintieId))]
    public class Associe
    {
        [Key]
        [Column("adr_id")]
        public int AdresseID { get; set; }

        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [ForeignKey(nameof(AdresseID))]
        [InverseProperty(nameof(Adresse.AResidents))]
        public virtual Adresse ResideA { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.VintiesResides))]
        public virtual Vintie ResideVintie { get; set; } = null!;
    }
}
