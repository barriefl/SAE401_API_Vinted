using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IAvisRepository : IDataRepository<Avis>
    {
        Task<ActionResult<IEnumerable<TypeAvis>>> GetAllTypesAvisAsync();
        Task<ActionResult<TypeAvis>> GetTypeAvisByIdAsync(int id);
    }
}
