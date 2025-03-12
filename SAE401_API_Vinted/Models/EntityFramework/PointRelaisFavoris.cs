using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_pointrelaisfavoris_prf")]
    [PrimaryKey(nameof(VintieId), nameof(PointRelaisID))]    
    public class PointRelaisFavoris
    {
        [Key]
        [Column("prf_vintieid")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieId { get; set; }

        [Key]
        [Column("prf_pointrelaisid")]
        [ForeignKey(nameof(PointRelais.PointRelaisID))]
        public int PointRelaisID { get; set; }

        [InverseProperty(nameof(Vintie.PointRelaisFavorisVintie))]
        public virtual Vintie VintiePointRelais { get; set; } = null!;

        [InverseProperty(nameof(PointRelais.))]
        public virtual PointRelais FavPointRelais { get; set; } = null!;
    }
}
