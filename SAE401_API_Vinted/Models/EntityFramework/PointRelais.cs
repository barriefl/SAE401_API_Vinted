using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework  
{
    [Table("t_e_pointrelais_ptr")]
    public class PointRelais
    {
        [Key]
        [Column("ptr_id")]
        public int PointRelaisID { get; set; }

        [Required]
        [Column("adr_id")]
        public int AdresseId { get; set; }

        [Required]
        [Column("ptr_nom")]
        [StringLength(40)]
        public string Nom { get; set; } = null!;

        [InverseProperty(nameof(Commande.ACommePointRelais))]
        public virtual ICollection<Commande> ADesCommandes { get; set; } = new List<Commande>();

        [ForeignKey(nameof(AdresseId))]
        [InverseProperty(nameof(Adresse.ADesPointRelais))]
        public virtual Adresse AdressePointRelais { get; set; } = null!;

        [InverseProperty(nameof(Horaire.ACommeHoraire))]
        public virtual ICollection<Horaire> HorairesPointRelais { get; set; } = new List<Horaire>();

        [InverseProperty(nameof(PointRelaisFavoris.FavPointRelais))]
        public virtual ICollection<PointRelaisFavoris> PointsRelaisEnFavoris { get; set; } = new List<PointRelaisFavoris>();

        public PointRelais()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is PointRelais relais &&
                   PointRelaisID == relais.PointRelaisID &&
                   AdresseId == relais.AdresseId &&
                   Nom == relais.Nom &&
                   EqualityComparer<ICollection<Commande>>.Default.Equals(ADesCommandes, relais.ADesCommandes) &&
                   EqualityComparer<Adresse>.Default.Equals(AdressePointRelais, relais.AdressePointRelais) &&
                   EqualityComparer<ICollection<Horaire>>.Default.Equals(HorairesPointRelais, relais.HorairesPointRelais) &&
                   EqualityComparer<ICollection<PointRelaisFavoris>>.Default.Equals(PointsRelaisEnFavoris, relais.PointsRelaisEnFavoris);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(PointRelaisID, AdresseId, Nom, ADesCommandes, AdressePointRelais, HorairesPointRelais, PointsRelaisEnFavoris);
        }
    }
}
