using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_signalement_sgn")]
    public class Signalement
    {
        [Key]
        [Column("sgn_id")]
        public int SignalementId { get; set; }

        [Required]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Required]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Required]
        [Column("sts_id")]
        public int StatusSignalementId { get; set; }

        [Required]
        [Column("mos_id")]
        public int MotifSignalementId { get; set; }

        [Required]
        [Column("sgn_commentaire")]
        [StringLength(300)]
        public string Commentaire { get; set; } = null!;

        [Required]
        [Column("sgn_dateouvertureticket", TypeName = "date")]
        public DateTime DateOuvertureTicket { get; set; }

        [Column("sgn_datefermetureticket", TypeName = "date")]
        public DateTime? DateFermetureTicket { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.SignalementsDeArticle))]
        public virtual Article ArticleDuSignalement { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.SignalementsDeArticle))]
        public virtual Vintie VintieSignalant { get; set; } = null!;

        [ForeignKey(nameof(StatusSignalementId))]
        [InverseProperty(nameof(StatusSignalement.StatusDesSignalements))]
        public virtual StatusSignalement StatusDuSignalement { get; set; } = null!;

        [ForeignKey(nameof(MotifSignalementId))]
        [InverseProperty(nameof(MotifSignalement.MotifsDesSignalement))]
        public virtual MotifSignalement MotifDuSignalement { get; set; } = null!;

        
    }
}
