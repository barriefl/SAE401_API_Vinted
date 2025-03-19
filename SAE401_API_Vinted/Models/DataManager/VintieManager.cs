using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class VintieManager : IArticleRepository<Vintie>
    {
        readonly VintedDBContext? vintiesDbContext;

        public VintieManager() { }

        public VintieManager(VintedDBContext context)
        {
            vintiesDbContext = context;
        }

        public async Task DeleteAsync(Vintie entity)
        {
            vintiesDbContext.Vinties.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<Vintie>>> GetAllAsync()
        {
            return await vintiesDbContext.Vinties.ToListAsync();
        }

        public async Task<ActionResult<Vintie>> GetByIdAsync(int id)
        {
            return await vintiesDbContext.Vinties.FirstOrDefaultAsync(u => u.VintieId == id);
        }

        public async Task<ActionResult<IEnumerable<Vintie>>> GetByStringAsync(string text)
        {
            var vinties = await vintiesDbContext.Vinties
             .Where(a =>
             a.Pseudo.ToUpper().Contains(text.ToUpper()))
             .ToListAsync();

            return vinties;
        }

        public async Task PostAsync(Vintie entity)
        {
            await vintiesDbContext.Vinties.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PutAsync(Vintie entityToUpdate, Vintie entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.VintieId = entity.VintieId;
            entityToUpdate.Pseudo = entity.Pseudo;
            entityToUpdate.TypeCompteId = entity.TypeCompteId;
            entityToUpdate.Nom = entity.Nom;
            entityToUpdate.Prenom = entity.Prenom;
            entityToUpdate.Civilite = entity.Civilite;
            entityToUpdate.Mail = entity.Mail;
            entityToUpdate.Pwd = entity.Pwd;
            entityToUpdate.Telephone = entity.Telephone;
            entityToUpdate.DateNaissance = entity.DateNaissance;
            entityToUpdate.URLPhoto = entity.URLPhoto;
            entityToUpdate.DateInscription = entity.DateInscription;
            entityToUpdate.MontantCompte = entity.MontantCompte;
            entityToUpdate.DateDerniereConnexion = entity.DateDerniereConnexion;
            entityToUpdate.Consentement = entity.Consentement;
            entityToUpdate.Siret = entity.Siret;
            entityToUpdate.VintieCodeNavigation = entity.VintieCodeNavigation;
            entityToUpdate.VintiesResides = entity.VintiesResides;
            entityToUpdate.ArticlesDuVendeur = entity.ArticlesDuVendeur;
            entityToUpdate.AppartienentVintie = entity.AppartienentVintie;
            entityToUpdate.ADesAvisVendeur = entity.ADesAvisVendeur;
            entityToUpdate.ADesAvisAcheteur = entity.ADesAvisAcheteur;
            entityToUpdate.PreferencesVintie = entity.PreferencesVintie;
            entityToUpdate.SignalementsDeArticle = entity.SignalementsDeArticle;
            entityToUpdate.FavorisDeVintie = entity.FavorisDeVintie;
            entityToUpdate.PointRelaisFavorisVintie = entity.PointRelaisFavorisVintie;
            entityToUpdate.CommandesVinties = entity.CommandesVinties;
            entityToUpdate.ConversationsAcheteur = entity.ConversationsAcheteur;
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
