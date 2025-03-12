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

        [Column("vnt_acheteurid")]
        public int AcheteurID { get; set; }

        [Column("vnt_vendeurid")]
        public int VendeurID { get; set; }

        [Column("avs_codetypeavis")]
        public int CodeTypeAvis { get; set; }

        [Column("avs_commentaire")]
        public string? Commentaire { get; set; }

        [Column("avs_note")]
        public decimal Note { get; set; }

        [ForeignKey(nameof(CodeTypeAvis))]
        [InverseProperty(nameof(TypeAvis.PossedesTypeAvis))]
        public virtual TypeAvis APourTypeAvis { get; set; } = null!;

        [ForeignKey(nameof(VendeurID))]
        [InverseProperty(nameof(Vintie.ADesAvis))]
        public virtual Vintie ApourVendeur { get; set; } = null!;

        [ForeignKey(nameof(AcheteurID))]
        [InverseProperty(nameof(Vintie.ADesAvis))]
        public virtual Vintie APourAcheteur { get; set; } = null!;
    }
}
