using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface ICategorieRepository : IGetDataRepository<Categorie>
    {
        Task<ActionResult<IEnumerable<Categorie>>> GetSousCategories(int idParent);

        Task<ActionResult<IEnumerable<Categorie>>> GetCategoriesSansSousCategories();

        Task<ActionResult<IEnumerable<Taille>>> GetTaillesByCategorie(int catId);

    }
}
