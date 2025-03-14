using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_message_msg")]
    public class Message
    {
        [Key]
        [Column("msg_id")]
        public int MessageId { get; set; }

        [Required]
        [Column("cnv_id")]
        public int ConversationId { get; set; }

        [Required]
        [Column("msg_idexpediteur")]
        public int ExpediteurId { get; set; }

        [Required]
        [Column("msg_contenu")]
        [StringLength(300)]
        public string Contenu { get; set; } = null!;

        [Required]
        [Column("msg_dateenvoi", TypeName = "date")]
        public DateTime? DateEnvoi { get; set; }

        [ForeignKey(nameof(ConversationId))]
        [InverseProperty(nameof(Conversation.Messages))]
        public virtual Conversation ConversationMessage { get; set; } = null!;
    }
}
