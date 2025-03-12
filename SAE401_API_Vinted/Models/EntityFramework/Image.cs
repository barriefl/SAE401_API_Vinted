using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_e_image_img")]
    public class Image
    {
        [Key]
        [Column("img_id")]
        public int ImageId { get; set; }

        [ForeignKey(nameof(Article.ArticleId))]
        [Column("img_articleid")]
        public int ArticleId { get; set; }

        [Required]
        [Column("img_url")]
        [StringLength(500)]
        public string Url { get; set; } = null!;

        [InverseProperty(nameof(Article.ImagesDeArticle))]
        public virtual Article ArticleDeImage { get; set; } = null!;
    }
}
