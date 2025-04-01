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
        [Column("art_id")]
        public int ArticleId { get; set; }

        [Key]
        [Column("vnt_id")]
        public int VintieId { get; set; }


        [ForeignKey(nameof(ArticleId))]
        [InverseProperty(nameof(Article.FavorisArticle))]
        public virtual Article EstFavoris { get; set; } = null!;


        [ForeignKey(nameof(VintieId))]
        [InverseProperty(nameof(Vintie.FavorisDeVintie))]
        public virtual Vintie FavorisVintie { get; set; } = null!;

        public override bool Equals(object? obj)
        {
            return obj is Favoris favoris &&
                   ArticleId == favoris.ArticleId &&
                   VintieId == favoris.VintieId &&
                   EqualityComparer<Article>.Default.Equals(EstFavoris, favoris.EstFavoris) &&
                   EqualityComparer<Vintie>.Default.Equals(FavorisVintie, favoris.FavorisVintie);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(ArticleId, VintieId);
        }

        public Favoris()
        {
        }

        public Favoris(int articleId, int vintieId, Article estFavoris, Vintie favorisVintie)
        {
            ArticleId = articleId;
            VintieId = vintieId;
            EstFavoris = estFavoris;
            FavorisVintie = favorisVintie;
        }

        public Favoris(int articleId, int vintieId)
        {
            ArticleId = articleId;
            VintieId = vintieId;
        }
    }
}
