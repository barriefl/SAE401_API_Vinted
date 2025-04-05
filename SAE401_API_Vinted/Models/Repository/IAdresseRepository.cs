using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IAdresseRepository : IDataRepository<Adresse>
    {
        Task<ActionResult<IEnumerable<TypeAdresse>>> GetAllTypesAdresseAsync();
        Task<ActionResult<TypeAdresse>> GetTypeAdresseByIdAsync(int id);
        Task<ActionResult<IEnumerable<Pays>>> GetAllPaysAsync();
        Task<ActionResult<Pays>> GetPaysByIdAsync(int id);
        Task<ActionResult<IEnumerable<Ville>>> GetAllVillesAsync();
        Task<ActionResult<Ville>> GetVilleByIdAsync(int id);
    }
}
