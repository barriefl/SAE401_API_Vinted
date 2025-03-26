using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IAdresseRepository : IDataRepository<Adresse>
    {

        Task<ActionResult<IEnumerable<TypeAdresse>>> GetAllTypesAdresseAsync();
        Task<ActionResult<TypeAdresse>> GetTypeAdresseByIdAsync(int id);
    }
}
