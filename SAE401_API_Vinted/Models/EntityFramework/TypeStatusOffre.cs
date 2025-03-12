using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typestatusoffre_tso")]
    public class TypeStatusOffre
    {
        [Key]
        [Column("tso_id")]
        public int TypeStatusOffreId { get; set; }

        [Column("tso_libelle")]
        [StringLength(20)]
        public string TypeStatusOffreLibelle { get; set; } = null!;

        [InverseProperty(nameof(Offre.EstTypeOffre))]
        public virtual ICollection<Offre> TypesOffres { get; set; } = new List<Offre>();
    }
}
