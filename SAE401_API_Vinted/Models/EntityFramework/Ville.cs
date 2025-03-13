using Microsoft.EntityFrameworkCore.Infrastructure;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_ville_vil")]
    public class Ville
    {
        [Key]
        [Column("vil_id")]
        public int VilleId { get; set; }

        [Required]
        [Column("pay_id")]
        public int PaysId { get; set; }

        [Required]
        [Column("vil_nom")]
        [StringLength(60)]
        public string Nom { get; set; } = null!;

        [Required]
        [Column("vil_codepostal", TypeName = "char(5)")]
        [RegularExpression(@"^\d{5}$", ErrorMessage = "Le code postal doit être composé de 5 chiffres.")]
        public int CodePostal { get; set; }

        [Column("vil_latitude")]
        public float? Latitude { get; set; }

        [Column("vil_longitude")]
        public float? Longitude { get; set; }

        [ForeignKey(nameof(PaysId))]
        [InverseProperty(nameof(Pays.VillesPays))]
        public virtual Pays PaysVille { get; set; } = null!;

        [InverseProperty(nameof(Adresse.VilleAdresse))]
        public virtual ICollection<Adresse> AdressesVilles { get; set; } = new List<Adresse>();
    }
}
