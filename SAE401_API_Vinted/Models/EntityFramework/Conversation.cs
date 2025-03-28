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

        public Conversation()
        {
        }

        public Conversation(int articleId, int acheteurId, Article articleIdNavigation, Vintie acheteurIdNavigation, ICollection<Message> messages)
        {
            ArticleId = articleId;
            AcheteurId = acheteurId;
            ArticleIdNavigation = articleIdNavigation;
            AcheteurIdNavigation = acheteurIdNavigation;
            Messages = messages;
        }

        public Conversation(int conversationId, int articleId, int acheteurId, Article articleIdNavigation, Vintie acheteurIdNavigation, ICollection<Message> messages)
        {
            ConversationId = conversationId;
            ArticleId = articleId;
            AcheteurId = acheteurId;
            ArticleIdNavigation = articleIdNavigation;
            AcheteurIdNavigation = acheteurIdNavigation;
            Messages = messages;
        }

        public override bool Equals(object? obj)
        {
            return obj is Conversation conversation &&
                   ConversationId == conversation.ConversationId &&
                   ArticleId == conversation.ArticleId &&
                   AcheteurId == conversation.AcheteurId &&
                   EqualityComparer<Article>.Default.Equals(ArticleIdNavigation, conversation.ArticleIdNavigation) &&
                   EqualityComparer<Vintie>.Default.Equals(AcheteurIdNavigation, conversation.AcheteurIdNavigation) &&
                   EqualityComparer<ICollection<Message>>.Default.Equals(Messages, conversation.Messages);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ConversationId, ArticleId, AcheteurId, ArticleIdNavigation, AcheteurIdNavigation, Messages);
        }
    }
}
