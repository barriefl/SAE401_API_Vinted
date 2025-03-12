using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_avis_avs")]
    public class Avis
    {
        [Key]
        [Column("avs_id")]
        public int AvisID { get; set; }

        [Column("avs_acheteurid")]
        [ForeignKey("fk_avs_vnt_acheteur")]
        public int AcheteurID { get; set; }

        [Column("avs_vendeurid")]
        [ForeignKey("fk_avs_vnt_vendeur")]
        public int VendeurID { get; set; }

        [Column("avs_codetypeavis")]
        [ForeignKey(nameof(TypeAvis.Code))]
        public int CodeTypeAvis { get; set; }

        [Column("avs_commentaire")]
        public string? Commentaire { get; set; }

        [Column("avs_note")]
        public decimal Note { get; set; }

        [InverseProperty(nameof(TypeAvis.PossedesTypeAvis))]
        public virtual TypeAvis APourTypeAvis { get; set; } = null!;

        [InverseProperty(nameof(Vintie.ADesAvis))]
        public virtual Vintie ApourVendeur { get; set; } = null!;

        [InverseProperty(nameof(Vintie.ADesAvis))]
        public virtual Vintie APourAcheteur { get; set; } = null!;
    }
}
