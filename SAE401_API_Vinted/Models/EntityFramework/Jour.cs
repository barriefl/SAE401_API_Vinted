using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_jour_jor")]
    public class Jour
    {
        [Key]
        [Column("jor_id")]
        public int JourId { get; set; }

        [Required]
        [Column("jor_libelle")]
        [StringLength(10)]
        public string Libelle { get; set; } = null!;

        [InverseProperty(nameof(Horaire.JourOuvert))]
        public virtual ICollection<Horaire> HeuresOuverts { get; set; } = new List<Horaire>();
    }
}
