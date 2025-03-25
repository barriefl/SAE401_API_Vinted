using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface ICategorieRepository : IGetDataRepository<Categorie>
    {
        Task<IEnumerable<Categorie>> GetSousCategories(int idParent);
    }
}
