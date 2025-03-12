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
        public int ConversationIdArticle { get; set; }

        [Required]
        [Column("vnt_idacheteur")]
        public int ConversationIdAcheteur { get; set; }

        [Required]
        [Column("vnt_idvendeur")]
        public int ConversationIdVendeur { get; set; }

        [ForeignKey(nameof(ConversationIdArticle))]
        [InverseProperty(nameof(Article.ConversationsArticle))]
        public virtual Article ArticleIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(ConversationIdAcheteur))]
        [InverseProperty(nameof(Vintie.ConversationsVinties))]
        public virtual Vintie AcheteurIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(ConversationIdVendeur))]
        [InverseProperty(nameof(Vintie.ConversationsVinties))]
        public virtual Vintie VendeurIdNavigation { get; set; } = null!;


        [InverseProperty(nameof(Message.Conversation))]
        public virtual ICollection<Message> Messages { get; set; } = new List<Message>();
    }
}
