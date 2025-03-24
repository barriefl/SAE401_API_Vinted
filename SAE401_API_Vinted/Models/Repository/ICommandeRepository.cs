using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface ICommandeRepository<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByVintieIdAsync(int id);
        Task<ActionResult<IEnumerable<TypeEnvoi>>> GetAllTypesEnvoiAsync();
        Task<ActionResult<TypeEnvoi>> GetTypeEnvoiByIdAsync(int id);
        Task<ActionResult<IEnumerable<FormatColis>>> GetAllFormatsColisAsync();
        Task<ActionResult<FormatColis>> GetFormatColisByIdAsync(int id);
    }
}
