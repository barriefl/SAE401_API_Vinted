using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IVintieRepository<TEntity> : IDataRepository<TEntity>
    {
        Task<ActionResult<IEnumerable<TEntity>>> GetByPseudoAsync(string text);
        Task<ActionResult<IEnumerable<TypeCompte>>> GetAllTypesCompteAsync();
        Task<ActionResult<TypeCompte>> GetTypeCompteByIdAsync(int id);
        Task<ActionResult<CompteBancaire>> GetCompteBancaireByIdAsync(int id);
        Task PostCompteBancaireAsync(CompteBancaire entity);
        Task PutCompteBancaireAsync(CompteBancaire entityToUpdate, CompteBancaire entity);
        Task DeleteCompteBancaireAsync(CompteBancaire entity);
    }
}
