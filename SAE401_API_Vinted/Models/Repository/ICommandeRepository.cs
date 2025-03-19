using Microsoft.AspNetCore.Mvc;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface ICommandeRepository<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByVintieIdAsync(int id);
    }
}
