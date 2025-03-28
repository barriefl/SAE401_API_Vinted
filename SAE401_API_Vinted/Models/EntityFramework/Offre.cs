using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_offre_ofr")]
    public class Offre : Message
    {
        [Required]
        [Column("sto_id")]
        public int StatusOffreId{ get; set; }

        [Required]
        [Range(0, 9999.99)]
        [Column("ofr_montant", TypeName = "numeric(6,2)")]
        public double Montant { get; set; }

        [ForeignKey(nameof(StatusOffreId))]
        [InverseProperty(nameof(StatusOffre.StatusOffres))]
        public virtual StatusOffre EstStatusOffre { get; set; } = null!;


        public Offre()
        {
        }

        public override bool Equals(object? obj)
        {
            return obj is Offre offre &&
                   base.Equals(obj) &&
                   MessageId == offre.MessageId &&
                   ConversationId == offre.ConversationId &&
                   ExpediteurId == offre.ExpediteurId &&
                   Contenu == offre.Contenu &&
                   DateEnvoi == offre.DateEnvoi &&
                   EqualityComparer<Conversation>.Default.Equals(ConversationMessage, offre.ConversationMessage) &&
                   StatusOffreId == offre.StatusOffreId &&
                   Montant == offre.Montant &&
                   EqualityComparer<StatusOffre>.Default.Equals(EstStatusOffre, offre.EstStatusOffre);
        }

        public override int GetHashCode()
        {
            HashCode hash = new HashCode();
            hash.Add(base.GetHashCode());
            hash.Add(MessageId);
            hash.Add(ConversationId);
            hash.Add(ExpediteurId);
            hash.Add(Contenu);
            hash.Add(DateEnvoi);
            hash.Add(ConversationMessage);
            hash.Add(StatusOffreId);
            hash.Add(Montant);
            hash.Add(EstStatusOffre);
            return hash.ToHashCode();
        }
    }
}
