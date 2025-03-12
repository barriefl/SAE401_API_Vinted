using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_reside_rsd")]
    [PrimaryKey(nameof(AdresseId), nameof(VintieId))]
    public class Reside
    {
        [Key]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.AResidents))]
        public virtual Adresse ResideA { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.VintiesResides))]
        public virtual Vintie ResideVintie { get; set; } = null!;
    }
}
