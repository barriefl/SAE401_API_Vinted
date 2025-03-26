using Microsoft.AspNetCore.Mvc;
using SAE401_API_Vinted.Models.EntityFramework;

namespace SAE401_API_Vinted.Models.Repository
{
    public interface IPointRelaisRepository : IGetDataRepository<PointRelais>
    {
        Task<ActionResult<IEnumerable<Jour>>> GetAllJoursAsync();
        Task<ActionResult<Jour>> GetJourByIdAsync(int id);
    }
}
