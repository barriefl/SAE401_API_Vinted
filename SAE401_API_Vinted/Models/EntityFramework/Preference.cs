using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_preference_pre")]
    [PrimaryKey(nameof(VintieId), nameof(ExpediteurId))]
    public class Preference
    {
        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [Key]
        [Column("exp_id")]
        public int ExpediteurId { get; set; }

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.PreferencesVintie))]
        public virtual Vintie VintieIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(ExpediteurId))]
        [InverseProperty(nameof(Expediteur.PreferencesExpediteur))]
        public virtual Expediteur ExpediteurIdNavigation { get; set; } = null!;
    }
}
