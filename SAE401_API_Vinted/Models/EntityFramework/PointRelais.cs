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

        [Column("ptr_idadresse")]
        [ForeignKey(nameof(Adresse.AdresseID))]
        public int AdresseID { get; set; }

        [Required]
        [Column("ptr_nom")]
        public string Nom { get; set; } = null!;


        [InverseProperty(nameof(Commande.ACommePointRelais))]
        public virtual ICollection<Commande> ADesCommandes { get; set; } = new List<Commande>();

        [InverseProperty(nameof(Adresse.ADesPointRelais))]
        public virtual Adresse AdressePointRelais { get; set; } = null!;

        [InverseProperty(nameof(Horaire.ACommeHoraire))]
        public virtual ICollection<Horaire> HorairesPointRelais { get; set; } = new List<Horaire>();
    }
}
