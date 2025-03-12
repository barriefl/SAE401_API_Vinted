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
        [Column("app_comptebancaireid")]
        [ForeignKey(nameof(CompteBancaire.CompteId))]
        public int CompteId { get; set; }

        [Key]
        [Column("app_vintieid")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieId { get; set; }

        [InverseProperty(nameof(CompteBancaire.AppartientCompte))]
        public virtual CompteBancaire CompteIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(Vintie.AppartienentVintie))]
        public virtual Vintie VintieIdNavigation { get; set; } = null!;
    }
}
