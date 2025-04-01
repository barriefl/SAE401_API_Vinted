using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_avis_avs")]
    public class Avis
    {
        [Key]
        [Column("avs_id")]
        public int AvisId { get; set; }

        [Required]
        [Column("vnt_acheteurid")]
        public int AcheteurId { get; set; }

        [Required]
        [Column("vnt_vendeurid")]
        public int VendeurId { get; set; }

        [Required]
        [Column("avs_codetypeavis")]
        public int CodeTypeAvis { get; set; }

        [Required]
        [Column("avs_commentaire")]
        [StringLength(300)]
        public string? Commentaire { get; set; }

        [Required]
        [Column("avs_note")]
        public decimal Note { get; set; }

        [ForeignKey(nameof(CodeTypeAvis))]
        [InverseProperty(nameof(TypeAvis.PossedesTypeAvis))]
        public virtual TypeAvis APourTypeAvis { get; set; } = null!;

        [ForeignKey(nameof(VendeurId))]
        [InverseProperty(nameof(Vintie.ADesAvisVendeur))]
        public virtual Vintie APourVendeur { get; set; } = null!;

        [ForeignKey(nameof(AcheteurId))]
        [InverseProperty(nameof(Vintie.ADesAvisAcheteur))]
        public virtual Vintie APourAcheteur { get; set; } = null!;

        public Avis()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Avis avis &&
                   AvisId == avis.AvisId &&
                   AcheteurId == avis.AcheteurId &&
                   VendeurId == avis.VendeurId &&
                   CodeTypeAvis == avis.CodeTypeAvis &&
                   Commentaire == avis.Commentaire &&
                   Note == avis.Note &&
                   EqualityComparer<TypeAvis>.Default.Equals(APourTypeAvis, avis.APourTypeAvis) &&
                   EqualityComparer<Vintie>.Default.Equals(APourVendeur, avis.APourVendeur) &&
                   EqualityComparer<Vintie>.Default.Equals(APourAcheteur, avis.APourAcheteur);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(AvisId);
            hash.Add(AcheteurId);
            hash.Add(VendeurId);
            hash.Add(CodeTypeAvis);
            hash.Add(Commentaire);
            hash.Add(Note);
            hash.Add(APourTypeAvis);
            hash.Add(APourVendeur);
            hash.Add(APourAcheteur);
            return hash.ToHashCode();
        }
    }
}
