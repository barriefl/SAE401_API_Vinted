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

        [Required]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Required]
        [Column("vnt_idacheteur")]
        public int AcheteurId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.ConversationsArticle))]
        public virtual Article ArticleIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(AcheteurId))]
        [InverseProperty(nameof(Vintie.ConversationsAcheteur))]
        public virtual Vintie AcheteurIdNavigation { get; set; } = null!;

        [InverseProperty(nameof(Message.ConversationMessage))]
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
