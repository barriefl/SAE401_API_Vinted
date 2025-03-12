using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_conversation_cnv")]
    public class Conversation
    {
        [Key]
        [Column("cnv_id")]
        public int ConversationId { get; set; }

        [ForeignKey(nameof(Article.ArticleId))]
        [Required]
        [Column("cnv_idarticle")]
        public int ConversationIdArticle { get; set; }

        [Required]
        [Column("cnv_idacheteur")]
        public int ConversationIdAcheteur { get; set; }

        [Required]
        [Column("cnv_idvendeur")]
        public int ConversationIdVendeur { get; set; }     

        [InverseProperty(nameof(Message.Conversation))]
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
