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

        public override bool Equals(object? obj)
        {
            return obj is Preference preference &&
                   VintieId == preference.VintieId &&
                   ExpediteurId == preference.ExpediteurId &&
                   EqualityComparer<Vintie>.Default.Equals(VintieIdNavigation, preference.VintieIdNavigation) &&
                   EqualityComparer<Expediteur>.Default.Equals(ExpediteurIdNavigation, preference.ExpediteurIdNavigation);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(VintieId, ExpediteurId);
        }

        public Preference()
        {
        }

        public Preference(int vintieId, int expediteurId, Vintie vintieIdNavigation, Expediteur expediteurIdNavigation)
        {
            VintieId = vintieId;
            ExpediteurId = expediteurId;
            VintieIdNavigation = vintieIdNavigation;
            ExpediteurIdNavigation = expediteurIdNavigation;
        }

        public Preference(int vintieId, int expediteurId)
        {
            VintieId = vintieId;
            ExpediteurId = expediteurId;
        }
    }
}
