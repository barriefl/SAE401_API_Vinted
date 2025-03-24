using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IJointureRepository<TEntity>
    {
        Task<ActionResult<TEntity>> GetByIdsAsync(int id1, int id2);
        Task PostAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
