using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeretour_tpr")]
    public class TypeRetour
    {
        [Key]
        [Column("tpr_id")]
        public int TypeRetourId { get; set; }

        [Required]
        [Column("tpr_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;


    }
}
