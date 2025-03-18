using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IDataRepositoryArticleVintie<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetAllAsync();
        Task<ActionResult<TEntity>> GetByIdAsync(int id);
        Task<ActionResult<IEnumerable<TEntity>>> GetByStringAsync(string text);
        Task PostAsync(TEntity entity);
        Task PutAsync(TEntity entityToUpdate, TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
