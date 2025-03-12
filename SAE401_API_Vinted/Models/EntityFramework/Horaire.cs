using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_horaire_hor")]
    [PrimaryKey(nameof(PointRelaisID), nameof(CodeJour))]
    public class Horaire
    {
        [Key]
        [Column("hor_idpointrelais")]
        [ForeignKey(nameof(PointRelais.PointRelaisID))]
        public int PointRelaisID { get; set; }

        [Key]
        [Column("hor_codejour")]
        [ForeignKey(nameof(Jour.Code))]
        public int CodeJour { get; set; }

        [Column("hor_heureouverture")]
        public DateTime HeureOuverture { get; set; }

        [Column("hor_heurefermeture")]
        public DateTime HeureFermeture { get; set; }

        [InverseProperty(nameof(PointRelais.HorairesPointRelais))]
        public virtual PointRelais ACommeHoraire { get; set; } = null!;

        [InverseProperty(nameof(Jour.HeuresOuverts))]
        public virtual Jour JourOuvert { get; set; } = null!;

    }
}
