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
        [ForeignKey(nameof(PointRelais.VilleID))]
        public int PointRelaisID { get; set; }

        [Column("cmd_idvintie")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieID { get; set; }

        [Column("cmd_codeexpediteur")]
        [ForeignKey(nameof(Expediteur.VilleID))]
        public int CodeExpediteur { get; set; }

        [Column("cmd_codeformat")]
        [ForeignKey(nameof(FormatColis.VilleID))]
        public int CodeFormat { get; set; }

        [Column("cmd_idarticle")]
        [ForeignKey(nameof(Article.ArticelId))]
        public int ArticleID { get; set; }

        [Column("cmd_typeenvoi")]
        public string? TypeEnvoi { get; set; }

        [Column("cmd_montant_total")]
        public decimal MontantTotal { get; set; }

    }
}
