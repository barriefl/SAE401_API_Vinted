using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IDataRepositoryCommande<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetByVintieIdAsync(int id);
        Task PostAsync(TEntity entity);
    }
}
