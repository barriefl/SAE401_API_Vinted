using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_avis_avs")]
    public class Avis
    {
        [Key]
        [Column("avs_id")]
        public int AvisId { get; set; }

        [Required]
        [Column("vnt_acheteurid")]
        public int AcheteurId { get; set; }

        [Required]
        [Column("vnt_vendeurid")]
        public int VendeurId { get; set; }

        [Required]
        [Column("avs_codetypeavis")]
        public int CodeTypeAvis { get; set; }

        [Required]
        [Column("avs_commentaire")]
        public string? Commentaire { get; set; }

        [Required]
        [Column("avs_note")]
        public decimal Note { get; set; }

        [ForeignKey(nameof(CodeTypeAvis))]
        [InverseProperty(nameof(TypeAvis.PossedesTypeAvis))]
        public virtual TypeAvis APourTypeAvis { get; set; } = null!;

        [ForeignKey(nameof(VendeurId))]
        [InverseProperty(nameof(Vintie.ADesAvisVendeur))]
        public virtual Vintie APourVendeur { get; set; } = null!;

        [ForeignKey(nameof(AcheteurId))]
        [InverseProperty(nameof(Vintie.ADesAvisAcheteur))]
        public virtual Vintie APourAcheteur { get; set; } = null!;
    }
}
