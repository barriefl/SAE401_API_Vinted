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

        [Required]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Required]
        [Column("exp_id")]
        public int ExpediteurId { get; set; }

        [Required]
        [Column("fmc_id")]
        public int CodeFormat { get; set; }

        [Required]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Required]
        [Column("tye_id")]
        public int TypeEnvoiId { get; set; }

        [Column("ptr_id")]
        public int? PointRelaisID { get; set; }

        [Required]
        [Column("cmd_montant_total")]
        public decimal MontantTotal { get; set; }

        [ForeignKey(nameof(CodeFormat))]
        [InverseProperty(nameof(FormatColis.ADesCommandes))]
        public virtual FormatColis ACommeFormat { get; set; } = null!;

        [ForeignKey(nameof(PointRelaisID))]
        [InverseProperty(nameof(PointRelais.ADesCommandes))]
        public virtual PointRelais ACommePointRelais { get; set; } = null!;

        [ForeignKey(nameof(ExpediteurId))]
        [InverseProperty(nameof(Expediteur.CommandesExpediteurs))]
        public virtual Expediteur ExpediteurCommande { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.CommandesVinties))]
        public virtual Vintie VintieCommande { get; set; } = null!;

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.CommandesArticles))]
        public virtual Article ArticleCommande { get; set; } = null!;

        [ForeignKey(nameof(TypeEnvoiId))]
        [InverseProperty(nameof(TypeEnvoi.TypeEnvoiCommandes))]
        public virtual TypeEnvoi TypeEnvoiDeCommande { get; set; } = null!;

        [InverseProperty(nameof(Transaction.CommandeTransaction))]
        public virtual ICollection<Transaction> TransactionsCommandes { get; set; } = new List<Transaction>();
    }
}
