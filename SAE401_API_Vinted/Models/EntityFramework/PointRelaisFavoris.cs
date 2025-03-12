using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_pointrelaisfavoris_prf")]
    [PrimaryKey(nameof(VintieId), nameof(PointRelaisId))]    
    public class PointRelaisFavoris
    {
        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Key]
        [Column("ptr_id")]
        public int PointRelaisId { get; set; }

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.PointRelaisFavorisVintie))]
        public virtual Vintie VintiePointRelais { get; set; } = null!;

        [ForeignKey(nameof(PointRelaisId))]
        [InverseProperty(nameof(PointRelais.PointsRelaisEnFavoris))]
        public virtual PointRelais FavPointRelais { get; set; } = null!;
    }
}
