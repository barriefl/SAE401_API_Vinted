using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_retour_ret")]
    public class Retour
    {
        [Key]
        [Column("ret_id")]
        public int RetourId { get; set; }

        [Required]
        [Column("ret_frais", TypeName = "numeric(6,2)")]
        public double Frais { get; set; }

        [Required]
        [Column("ret_datedemande", TypeName = "date")]
        public DateTime DateDemande { get; set; }

        [Column("ret_dateenvoi", TypeName = "date")]
        public DateTime DateEnvoi { get; set; }

        [Column("ret_dateconfirmation", TypeName = "date")]
        public DateTime DateConfirmation { get; set; }

        [Required]
        [Column("ret_motif")]
        [StringLength(500)]
        public string Motif { get; set; } = null!;
    }
}
