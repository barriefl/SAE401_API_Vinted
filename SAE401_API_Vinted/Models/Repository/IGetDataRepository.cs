using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IGetDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
    }
}
