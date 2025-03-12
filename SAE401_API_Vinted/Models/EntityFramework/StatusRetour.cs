using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_statusretour_str")]
    public class StatusRetour
    {
        [Key]
        [Column("str_id")]
        public int StatusRetourId { get; set; }

        [Required]
        [Column("str_libelle")]
        [StringLength(50)]
        public string Libelle { get; set; } = null!;
    }
}
