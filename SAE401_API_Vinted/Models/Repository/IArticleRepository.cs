using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IArticleRepository : IDataRepository<Article>
    {
        Task<ActionResult<IEnumerable<Article>>> GetByStringAsync(string text);

        Task PutLikeAsync(int id, int compteur);

        Task<ActionResult<IEnumerable<Couleur>>> GetAllCouleursAsync();
        Task<ActionResult<Couleur>> GetCouleurByIdAsync(int id);
        Task<ActionResult<IEnumerable<Matiere>>> GetAllMatieresAsync();
        Task<ActionResult<Matiere>> GetMatiereByIdAsync(int id);
        Task<ActionResult<IEnumerable<Taille>>> GetAllTaillesAsync();
        Task<ActionResult<Taille>> GetTailleByIdAsync(int id);
        Task<ActionResult<IEnumerable<Marque>>> GetAllMarquesAsync();
        Task<ActionResult<Marque>> GetMarqueByIdAsync(int id);
        Task<ActionResult<IEnumerable<EtatArticle>>> GetAllEtatsArticlesAsync();
        Task<ActionResult<EtatArticle>> GetEtatArticleByIdAsync(int id);
        Task<ActionResult<IEnumerable<EtatVente>>> GetAllEtatsVentesAsync();
        Task<ActionResult<EtatVente>> GetEtatVenteByIdAsync(int id);
    }
}
