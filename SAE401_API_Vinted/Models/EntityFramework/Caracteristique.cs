using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_caracteristique_car")]
    public class Caracteristique
    {
        [Key]
        [Column("car_id")]
        public int CaracteristiqueId { get; set; }

        [Column("car_nom")]
        [StringLength(40)]
        public string Nom { get; set; }

        [InverseProperty(nameof(Taille.CaracteristiqueIdNavigation))]
        public virtual ICollection<Taille> TaillesCaracteristique { get; set; } = new List<Taille>();
    }
}
