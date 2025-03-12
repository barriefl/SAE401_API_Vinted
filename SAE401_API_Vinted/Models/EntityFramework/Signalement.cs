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

        [ForeignKey(nameof(Article.ArticleId))]
        [Column("sgn_articleid")]
        public int ArticleId { get; set; }

        [ForeignKey(nameof(Vintie.VintieId))]
        [Column("sgn_vintieid")]
        public int VintieId { get; set; }

        [ForeignKey(nameof(StatusSignalement.StatusSignalementId))]
        [Column("sgn_statussignalementid")]
        public int StatusSignalementId { get; set; }

        [ForeignKey(nameof(MotifSignalement.MotifSignalementId))]
        [Column("sgn_motifsignalementid")]
        public int MotifSignalementId { get; set; }

        [Required]
        [Column("sgn_dateouvertureticket", TypeName = "date")]
        public DateTime DateOuvertureTicket { get; set; }

        [Required]
        [Column("sgn_datefermetureticket", TypeName = "date")]
        public DateTime DateFermetureTicket { get; set; }

        [Required]
        [Column("sgn_commentaire")]
        [StringLength(300)]
        public string Commentaire { get; set; } = null!;

        [InverseProperty(nameof(StatusSignalement.StatusDesSignalements))]
        public virtual StatusSignalement StatusDuSignalement { get; set; } = null!;

        [InverseProperty(nameof(MotifSignalement.MotifsDesSignalement))]
        public virtual MotifSignalement MotifDuSignalement { get; set; } = null!;

        [InverseProperty(nameof(Vintie.SignalementsDeArticle))]
        public virtual Vintie VintieSignalant { get; set; } = null!;
    }
}
