using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_pays_pay")]
    public class Pays
    {
        [Key]
        [Column("pay_id")]
        public int PaysId { get; set; }

        [Required]
        [Column("pay_libelle")]
        [StringLength(100)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Ville.PaysVille))]
        public virtual ICollection<Ville> VillesPays { get; set; } = new List<Ville>();
    }
}
