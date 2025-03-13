using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_formatcolis_fmc")]
    public class FormatColis
    {
        [Key]
        [Column("fmc_id")]
        public int Code { get; set; }

        [Required]
        [Column("fmc_lib")]
        [StringLength(40)]
        public string Libelle { get; set; } = null!;

        [Column("fmc_fraissupplementaire")]
        public decimal? FraisSupplementaire { get; set; }

        [InverseProperty(nameof(Commande.ACommeFormat))]
        public virtual ICollection<Commande> ADesCommandes { get; set; } = new List<Commande>();
    }
}
