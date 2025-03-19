using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IArticleRepository<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByStringAsync(string text);
    }
}
