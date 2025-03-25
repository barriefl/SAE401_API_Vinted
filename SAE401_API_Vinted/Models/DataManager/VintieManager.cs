using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

namespace SAE401_API_Vinted.Models.DataManager
{
    public class VintieManager : IVintieRepository<Vintie>
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
            return await vintiesDbContext.Vinties
                .Include(a => a.VintiesResides)
                    .ThenInclude(a => a.ResideA)
                        .ThenInclude(a => a.VilleAdresse)
                            .ThenInclude(a => a.PaysVille)
                .Include(a => a.ArticlesDuVendeur)
                .Include(a => a.AppartienentVintie)
                    .ThenInclude(a => a.CompteIdNavigation)
                .Include(a => a.ADesAvisVendeur)
                    .ThenInclude(a => a.APourTypeAvis)
                .Include(a => a.ADesAvisAcheteur)
                .Include(a => a.PreferencesVintie)
                    .ThenInclude(a => a.ExpediteurIdNavigation)
                .Include(a => a.SignalementsDeArticle)
                .Include(a => a.FavorisDeVintie)
                .Include(a => a.PointRelaisFavorisVintie)
                .Include(a => a.CommandesVinties)
                .Include(a => a.ConversationsAcheteur)
                .Include(a => a.RetourDesVintie)
                .FirstOrDefaultAsync(u => u.VintieId == id);
        }

        public async Task<ActionResult<IEnumerable<Vintie>>> GetByPseudoAsync(string text)
        {
            var vinties = await vintiesDbContext.Vinties
            .Where(a => a.Pseudo.ToUpper().Contains(text.ToUpper()))
                .Include(a => a.VintiesResides)
                    .ThenInclude(a => a.ResideA)
                        .ThenInclude(a => a.VilleAdresse)
                            .ThenInclude(a => a.PaysVille)
                .Include(a => a.ArticlesDuVendeur)
                .Include(a => a.AppartienentVintie)
                    .ThenInclude(a => a.CompteIdNavigation)
                .Include(a => a.ADesAvisVendeur)
                    .ThenInclude(a => a.APourTypeAvis)
                .Include(a => a.ADesAvisAcheteur)
                .Include(a => a.PreferencesVintie)
                    .ThenInclude(a => a.ExpediteurIdNavigation)
                .Include(a => a.SignalementsDeArticle)
                .Include(a => a.FavorisDeVintie)
                .Include(a => a.PointRelaisFavorisVintie)
                .Include(a => a.CommandesVinties)
                .Include(a => a.ConversationsAcheteur)
                .Include(a => a.RetourDesVintie)
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
            entityToUpdate.RetourDesVintie = entity.RetourDesVintie;
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task<ActionResult<IEnumerable<TypeCompte>>> GetAllTypesCompteAsync()
        {
            return await vintiesDbContext.TypesComptes.ToListAsync();
        }

        public async Task<ActionResult<TypeCompte>> GetTypeCompteByIdAsync(int id)
        {
            return await vintiesDbContext.TypesComptes
                .Include(tc => tc.VintiesType)
                .FirstOrDefaultAsync(tc => tc.TypeCompteId == id);
        }

        public async Task<ActionResult<CompteBancaire>> GetCompteBancaireByIdAsync(int id)
        {
            return await vintiesDbContext.ComptesBancaires
                .FirstOrDefaultAsync(cb => cb.CompteId == id);
        }

        public async Task PutCompteBancaireAsync(CompteBancaire entityToUpdate, CompteBancaire entity)
        {
            vintiesDbContext.Entry(entityToUpdate).State = EntityState.Modified;
            entityToUpdate.CompteId = entity.CompteId;
            entityToUpdate.Iban = entity.Iban;
            entityToUpdate.NomTitulaire = entity.NomTitulaire;
            entityToUpdate.PrenomTitulaire = entity.PrenomTitulaire;
            await vintiesDbContext.SaveChangesAsync();
        }

        public async Task PostCompteBancaireAsync(CompteBancaire entity)
        {
            await vintiesDbContext.ComptesBancaires.AddAsync(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
        public async Task DeleteCompteBancaireAsync(CompteBancaire entity)
        {
            vintiesDbContext.ComptesBancaires.Remove(entity);
            await vintiesDbContext.SaveChangesAsync();
        }
    }
}
