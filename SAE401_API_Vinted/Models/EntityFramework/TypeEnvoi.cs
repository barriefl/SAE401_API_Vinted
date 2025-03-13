using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_typeenvoi_tye")]
    public class TypeEnvoi
    {
        [Key]
        [Column("tye_id")]
        public int TypeEnvoiId { get; set; }

        [Column("tye_libelle")]
        [StringLength(15)]
        public int Libelle { get; set; }

        [InverseProperty(nameof(Commande.TypeEnvoiDeCommande))]
        public virtual ICollection<Commande> TypeEnvoiCommandes { get; set; } = new List<Commande>();
    }
}
