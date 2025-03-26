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

        public Adresse()
        {
        }

        public Adresse(int adresseID, int villeID, string libelle, Ville villeAdresse, ICollection<Possede> possedesAdresse, ICollection<Reside> aResidents, ICollection<PointRelais> aDesPointRelais)
        {
            AdresseID = adresseID;
            VilleID = villeID;
            Libelle = libelle;
            VilleAdresse = villeAdresse;
            PossedesAdresse = possedesAdresse;
            AResidents = aResidents;
            ADesPointRelais = aDesPointRelais;
        }

        public Adresse(int adresseID, int villeID, string libelle)
        {
            AdresseID = adresseID;
            VilleID = villeID;
            Libelle = libelle;
        }

        public override bool Equals(object? obj)
        {
            return obj is Adresse adresse &&
                   AdresseID == adresse.AdresseID &&
                   VilleID == adresse.VilleID &&
                   Libelle == adresse.Libelle &&
                   EqualityComparer<Ville>.Default.Equals(VilleAdresse, adresse.VilleAdresse) &&
                   EqualityComparer<ICollection<Possede>>.Default.Equals(PossedesAdresse, adresse.PossedesAdresse) &&
                   EqualityComparer<ICollection<Reside>>.Default.Equals(AResidents, adresse.AResidents) &&
                   EqualityComparer<ICollection<PointRelais>>.Default.Equals(ADesPointRelais, adresse.ADesPointRelais);
        }
    }
}
