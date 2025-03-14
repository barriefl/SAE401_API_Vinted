using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_taillearticle_tar")]
    [PrimaryKey(nameof(ArticleId), nameof(TailleId))]
    public class TailleArticle
    {
        [Key]
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Key]
        [Column("tal_id")]
        public int TailleId { get; set; }

        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.TaillesArticle))]
        public virtual Article ArticleIdNavigation { get; set; } = null!;

        [ForeignKey(nameof(TailleId))]
        [InverseProperty(nameof(Taille.ArticlesTaille))]
        public virtual Taille TailleIdNavigation { get; set; } =null!;
    }
}
