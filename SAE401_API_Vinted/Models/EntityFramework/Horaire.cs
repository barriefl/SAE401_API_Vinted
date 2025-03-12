using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_horaire_hor")]
    [PrimaryKey(nameof(PointRelaisID), nameof(JourId))]
    public class Horaire
    {
        [Key]
        [Column("ptr_id")]
        public int PointRelaisID { get; set; }

        [Key]
        [Column("jor_id")]
        public int JourId { get; set; }

        [Column("hor_heureouverture")]
        public DateTime HeureOuverture { get; set; }

        [Column("hor_heurefermeture")]
        public DateTime HeureFermeture { get; set; }

        [ForeignKey(nameof(PointRelaisID))]
        [InverseProperty(nameof(PointRelais.HorairesPointRelais))]
        public virtual PointRelais ACommeHoraire { get; set; } = null!;

        [ForeignKey(nameof(JourId))]
        [InverseProperty(nameof(Jour.HeuresOuverts))]
        public virtual Jour JourOuvert { get; set; } = null!;

    }
}
