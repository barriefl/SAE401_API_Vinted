using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_adresse_adr")]
    public class Adresse
    {
        [Key]
        [Column("adr_id")]
        public int AdresseID { get; set; }

        [Required]
        [Column("vil_id")]
        public int VilleID { get; set; }

        [Required]
        [Column("adr_libelle")]
        [StringLength(200)]
        public string Libelle { get; set; } = null!;

        [ForeignKey(nameof(VilleID))]
        [InverseProperty(nameof(Ville.AdressesVilles))]
        public virtual Ville VilleAdresse { get; set; } = null!;

        [InverseProperty(nameof(Possede.APourAdresse))]
        public virtual ICollection<Possede> PossedesAdresse { get; set; } = new List<Possede>();

        [InverseProperty(nameof(Reside.ResideA))]
        public virtual ICollection<Reside> AResidents { get; set; } = new List<Reside>();

        [InverseProperty(nameof(PointRelais.AdressePointRelais))]
        public virtual ICollection<PointRelais> ADesPointRelais { get; set; } = new List<PointRelais>();
        
    }
}
