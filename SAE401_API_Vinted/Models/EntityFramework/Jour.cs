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

        [Column("jor_libelle")]
        public int Libelle { get; set; }

        [InverseProperty(nameof(Horaire.JourOuvert))]
        public virtual ICollection<Horaire> HeuresOuverts { get; set; } = new List<Horaire>();
    }
}
