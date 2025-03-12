using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    [Table("t_j_favoris_fav")]
    [PrimaryKey(nameof(ArticleId), nameof(VintieId))]
    public class Favoris
    {
        [Key]
        [Column("fav_idarticle")]
        [ForeignKey(nameof(Article.ArticleId))]
        public int ArticleId { get; set; }

        [Key]
        [Column("fav_idvintie")]
        [ForeignKey(nameof(Vintie.VintieId))]
        public int VintieId { get; set; }

        [InverseProperty(nameof(Article.FavorisArticle))]
        public virtual Article EstFavoris { get; set; } = null!;

        [InverseProperty(nameof(Vintie.FavorisDeVintie))]
        public virtual Vintie FavorisVintie { get; set; } = null!;
    }
}
