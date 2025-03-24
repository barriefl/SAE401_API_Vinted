namespace SAE401_API_Vinted.Models.Repository
{
    public interface IJointureRepository<TEntity>
    {
        Task PostAsync(TEntity entity);
        Task DeleteAsync(TEntity entity);
    }
}
