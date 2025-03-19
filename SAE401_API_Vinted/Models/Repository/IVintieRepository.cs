using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IVintieRepository<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByPseudoAsync(string text);
    }
}
