using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_transaction_tsc")]
    public class Transaction
    {
        [Key]
        [Column("tsc_id")]
        public int TransactionID { get; set; }

        [Required]
        [Column("tsc_idcommande")]
        public int CommandeID { get; set; }

        [Required]
        [Column("tsc_statustransaction")]
        [StringLength(30)]
        public string StatusTransaction { get; set; }

        [Required]
        [Column("tsc_datetransaction")]
        public DateTime DateTransaction { get; set; }

        [InverseProperty(nameof(Commande.TransactionsCommandes))]
        public virtual Commande CommandeTransaction { get; set; } = null!;        
    }
}
