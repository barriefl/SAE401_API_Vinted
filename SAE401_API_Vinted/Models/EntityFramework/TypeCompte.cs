using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typecompte_tyc")]
    public class TypeCompte
    {
        [Key]
        [Column("tyc_id")]
        public int Code { get; set; }

        [Column("tyc_libelle")]
        [StringLength(40)]
        public string Libelle { get; set; }

        [InverseProperty("VintieCodeNavigation")]
        public virtual ICollection<Vintie> VintiesType { get; set; } = new List<Vintie>();
    }
}
