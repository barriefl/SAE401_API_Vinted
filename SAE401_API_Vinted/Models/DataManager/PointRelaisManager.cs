using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class PointRelaisManager : IPointRelaisRepository<PointRelais>
    {
        readonly VintedDBContext? vintiesDbContext;

        public PointRelaisManager()
        {
        }

        public PointRelaisManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task<ActionResult<IEnumerable<PointRelais>>> GetAllAsync()
        {
            return await vintiesDbContext.PointsRelais.ToListAsync();
        }

        public async Task<ActionResult<PointRelais>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.PointsRelais
                .Include(c => c.ADesCommandes)
                .Include(c => c.AdressePointRelais)
                .Include(c => c.HorairesPointRelais).ThenInclude(c => c.JourOuvert)
                .Include(c => c.PointsRelaisEnFavoris).ThenInclude(c => c.VintiePointRelais)
                .FirstOrDefaultAsync(c => c.PointRelaisID == id);
        }

        public async Task<ActionResult<IEnumerable<Jour>>> GetAllJoursAsync()
        {
            return await vintiesDbContext.Jours.ToListAsync();
        }

        public async Task<ActionResult<Jour>> GetJourByIdAsync(int id)
        {
            return await vintiesDbContext.Jours
                .Include(j => j.HeuresOuverts)
                .FirstOrDefaultAsync(j => j.JourId == id);
        }
    }
}
