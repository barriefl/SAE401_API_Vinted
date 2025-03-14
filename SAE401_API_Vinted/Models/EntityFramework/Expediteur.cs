using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_expediteur_exp")]
    public class Expediteur
    {
        [Key]
        [Column("exp_id")]
        public int ExpediteurId { get; set; }

        [Column("exp_nom")]
        [StringLength(50)]
        public string Nom { get; set; }

        [InverseProperty(nameof(Preference.ExpediteurIdNavigation))]
        public virtual ICollection<Preference> PreferencesExpediteur { get; set; } = new List<Preference>();

        [InverseProperty(nameof(Commande.ExpediteurCommande))]
        public virtual ICollection<Commande> CommandesExpediteurs { get; set; } = new List<Commande>();

    }
}
