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

        [ForeignKey(nameof(Conversation.QuelqueChose))]
        [Required]
        [Column("msg_idexpediteur")]
        public int MessageIdExpediteur { get; set; }

        [Required]
        [Column("msg_iddestinataire")]
        public int MessageIdDestinataire { get; set; }

        [Required]
        [Column("msg_contenu")]
        [StringLength(300)]
        public string MessageContenu { get; set; } = null!;

        [Column("msg_dateenvoi", TypeName = "date")]
        public DateTime? MessageDateEnvoi { get; set; }

        [InverseProperty(nameof(Conversation.QuelqueChose))]
        public virtual Conversation QuelqueChose { get; set; } = null!;
    }
}
