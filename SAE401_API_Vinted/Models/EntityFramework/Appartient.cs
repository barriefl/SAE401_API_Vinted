using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_appartient_app")]
    [PrimaryKey(nameof(CompteId), nameof(VintieId))]
    public class Appartient
    {
        [Key]
        [Column("cob_id")]
        public int CompteId { get; set; }

        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }

        [ForeignKey(nameof(CompteId))]
        [InverseProperty(nameof(CompteBancaire.AppartientCompte))]
        public virtual CompteBancaire CompteIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.AppartienentVintie))]
        public virtual Vintie VintieIdNavigation { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            return obj is Appartient appartient &&
                   CompteId == appartient.CompteId &&
                   VintieId == appartient.VintieId &&
                   EqualityComparer<CompteBancaire>.Default.Equals(CompteIdNavigation, appartient.CompteIdNavigation) &&
                   EqualityComparer<Vintie>.Default.Equals(VintieIdNavigation, appartient.VintieIdNavigation);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(CompteId, VintieId);
        }

        public Appartient()
        {
        }

        public Appartient(int compteId, int vintieId, CompteBancaire compteIdNavigation, Vintie vintieIdNavigation)
        {
            CompteId = compteId;
            VintieId = vintieId;
            CompteIdNavigation = compteIdNavigation;
            VintieIdNavigation = vintieIdNavigation;
        }

        public Appartient(int compteId, int vintieId)
        {
            CompteId = compteId;
            VintieId = vintieId;
        }
    }
}
