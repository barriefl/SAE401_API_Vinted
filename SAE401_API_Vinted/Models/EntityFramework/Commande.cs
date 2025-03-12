using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_commande_cmd")]
    public class Commande
    {
        [Key]
        [Column("cmd_id")]
        public int CommandeID { get; set; }

        [Column("cmd_idpointrelais")]
        [ForeignKey(nameof(PointRelais.PointRelaisID))]
        public int PointRelaisID { get; set; }

        [Column("cmd_idvintie")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieID { get; set; }

        [Column("cmd_codeexpediteur")]
        [ForeignKey(nameof(Expediteur.ExpediteurId))]
        public int CodeExpediteur { get; set; }

        [Column("cmd_codeformat")]
        [ForeignKey(nameof(FormatColis.Code))]
        public int CodeFormat { get; set; }

        [Column("cmd_idarticle")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleID { get; set; }

        [Column("cmd_typeenvoi")]
        [StringLength(20)]
        public string? TypeEnvoi { get; set; }

        [Column("cmd_montant_total")]
        public decimal MontantTotal { get; set; }


        [InverseProperty(nameof(FormatColis.ADesCommandes))]
        public virtual FormatColis ACommeFormat { get; set; } = null!;
        

        [InverseProperty(nameof(PointRelais.ADesCommandes))]
        public virtual PointRelais ACommePointRelais { get; set; } = null!;

        [InverseProperty(nameof(Expediteur.CommandesExpediteurs))]
        public virtual Expediteur ExpediteurCommande { get; set; } = null!;
    }
}
