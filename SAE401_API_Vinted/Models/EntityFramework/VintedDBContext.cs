using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace SAE401_API_Vinted.Models.EntityFramework
{
    public partial class VintedDBContext : DbContext
    {
        public VintedDBContext() {}

        public VintedDBContext(DbContextOptions<VintedDBContext> options) : base(options) { }

        public virtual DbSet<Adresse> Adresses { get; set; }

        public virtual DbSet<Appartient> Appartient { get; set; }

        public virtual DbSet<Article> Articles { get; set; }

        public virtual DbSet<Avis> Avis { get; set; }

        public virtual DbSet<CarteBancaire> CartesBancaires { get; set; }

        public virtual DbSet<Categorie> Categories { get; set; }

        public virtual DbSet<Commande> Commandes { get; set; }

        public virtual DbSet<CompteBancaire> ComptesBancaires { get; set; }

        public virtual DbSet<Conversation> Conversations { get; set; }

        public virtual DbSet<Couleur> Couleurs { get; set; }

        public virtual DbSet<CouleurArticle> CouleursArticles { get; set; }

        public virtual DbSet<EtatArticle> EtatsArticles { get; set; }

        public virtual DbSet<EtatVente> EtatsVentesArticles { get; set; }

        public virtual DbSet<Expediteur> Expediteurs { get; set; }

        public virtual DbSet<Favoris> Favoris { get; set; }

        public virtual DbSet<FormatColis> FormatColis { get; set; }

        public virtual DbSet<Horaire> Horaires { get; set; }

        public virtual DbSet<Image> Images { get; set; }

        public virtual DbSet<Jour> Jours { get; set; }

        public virtual DbSet<Marque> Marques { get; set; }

        public virtual DbSet<Matiere> Matieres { get; set; }

        public virtual DbSet<MatiereArticle> MatieresArticles { get; set; }

        public virtual DbSet<Message> Messages { get; set; }

        public virtual DbSet<MotifSignalement> MotifsSignalements { get; set; }

        public virtual DbSet<Offre> Offres { get; set; }

        public virtual DbSet<Pays> Pays { get; set; }

        public virtual DbSet<PointRelais> PointsRelais { get; set; }

        public virtual DbSet<PointRelaisFavoris> PointsRelaisFavoris { get; set; }

        public virtual DbSet<Possede> Possede { get; set; }

        public virtual DbSet<Preference> Preferences { get; set; }

        public virtual DbSet<Reside> Reside { get; set; }

        public virtual DbSet<Retour> Retours { get; set; }

        public virtual DbSet<Signalement> Signalements { get; set; }

        public virtual DbSet<StatusOffre> StatusOffres { get; set; }

        public virtual DbSet<StatusRetour> StatusRetours { get; set; }

        public virtual DbSet<StatusSignalement> StatusSignalements { get; set; }

        public virtual DbSet<Taille> Tailles { get; set; }

        public virtual DbSet<TailleArticle> TaillesArticles { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TypeAdresse> TypesAdresses { get; set; }

        public virtual DbSet<TypeAvis> TypesAvis { get; set; }

        public virtual DbSet<TypeCompte> TypesComptes { get; set; }

        public virtual DbSet<TypeRetour> TypesRetours { get; set; }

        public virtual DbSet<TypeTaille> TypeTailles { get; set; }
                                                                                                                 
        public virtual DbSet<Ville> Villes { get; set; }

        public virtual DbSet<Vintie> Vinties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost; Database=VintedDB; Username=postgres; Password=postgres");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Adresse>(entity =>
            {
               entity.HasKey(e => e.AdresseID)
                .HasName("pk_adr");

                entity.HasOne(d => d.VilleAdresse)
                .WithMany(p => p.AdressesVilles)
                .HasForeignKey(d => d.VilleID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adr_vil");
            });

            modelBuilder.Entity<Appartient>(entity =>
            {
                entity.HasKey(e => new { e.CompteId, e.VintieId })
                 .HasName("pk_app");

                entity.HasOne(d => d.CompteIdNavigation)
                .WithMany(p => p.AppartientCompte)
                .HasForeignKey(d => d.CompteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_app_cob");

                entity.HasOne(d => d.VintieIdNavigation)
                .WithMany(p => p.AppartienentVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_app_vnt");
            });


            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                .HasName("pk_art");
                entity.Property(e => e.DateAjout).HasDefaultValueSql("now()");

                entity.HasOne(d => d.EtatDeArticle)
                .WithMany(p => p.EtatsDesArticles)
                .HasForeignKey(d => d.EtatArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_art_eta");

                entity.HasOne(d => d.VendeurDeArticle)
                .WithMany(p => p.ArticlesDuVendeur)
                .HasForeignKey(d => d.VendeurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_art_vnt");

                entity.HasOne(d => d.MarqueDeArticle)
                .WithMany(p => p.MarquesDesArticles)
                .HasForeignKey(d => d.MarqueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_art_mrq");

                entity.HasOne(d => d.EtatVenteDeArticle)
                .WithMany(p => p.EtatsVenteDesArticles)
                .HasForeignKey(d => d.EtatVenteArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_art_eva");

                entity.HasOne(d => d.CategorieDeArticle)
                .WithMany(p => p.CategoriesArticles)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_art_cat");

                entity.Property(e => e.EtatArticleId).HasDefaultValue(3);

                entity.Property(e => e.MarqueId).HasDefaultValue(59);

            });

            modelBuilder.Entity<Avis>(entity =>
            {
                entity.HasKey(e => e.AvisId)
                 .HasName("pk_avs");

                entity.HasOne(d => d.APourTypeAvis)
                .WithMany(p => p.PossedesTypeAvis)
                .HasForeignKey(d => d.CodeTypeAvis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avs_tas");

                entity.HasOne(d => d.APourVendeur)
                .WithMany(p => p.ADesAvisVendeur)
                .HasForeignKey(d => d.VendeurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avs_vendeurvnt");

                entity.HasOne(d => d.APourAcheteur)
                .WithMany(p => p.ADesAvisAcheteur)
                .HasForeignKey(d => d.AcheteurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avs_acheteurvnt");
            });

            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.HasKey(e => e.CarteId)
                 .HasName("pk_cab");

                entity.HasOne(d => d.CompteIdNavigation)
                .WithMany(p => p.CartesCompte)
                .HasForeignKey(d => d.CompteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cab_cob");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.CategorieId)
                 .HasName("pk_cat");

                entity.HasOne(d => d.CategorieParentIdNavigation)
                .WithMany(p => p.CategoriesParent)
                .HasForeignKey(d => d.IdParent)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cat_cat");
            });


            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.CommandeID)
                 .HasName("pk_cmd");

                entity.HasOne(d => d.ACommeFormat)
                .WithMany(p => p.ADesCommandes)
                .HasForeignKey(d => d.CodeFormat)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_fmc");

                entity.HasOne(d => d.ACommePointRelais)
                .WithMany(p => p.ADesCommandes)
                .HasForeignKey(d => d.CodeFormat)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_ptr");

                entity.HasOne(d => d.ExpediteurCommande)
                .WithMany(p => p.CommandesExpediteurs)
                .HasForeignKey(d => d.ExpediteurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_exp");

                entity.HasOne(d => d.VintieCommande)
                .WithMany(p => p.CommandesVinties)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_vnt");

                entity.HasOne(d => d.ArticleCommande)
                .WithMany(p => p.CommandesArticles)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_art");

                entity.HasOne(d => d.TypeEnvoiDeCommande)
                .WithMany(p => p.TypeEnvoiCommandes)
                .HasForeignKey(d => d.TypeEnvoiId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cmd_tye");
            });



            modelBuilder.Entity<CompteBancaire>(entity =>
            {
                entity.HasKey(e => e.CompteId)
                .HasName("pk_cob");

            });

            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.HasKey(e => e.ConversationId)
                .HasName("pk_cnv");

                entity.HasOne(d => d.ArticleIdNavigation)
                .WithMany(p => p.ConversationsArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cnv_art");

                entity.HasOne(d => d.AcheteurIdNavigation)
                .WithMany(p => p.ConversationsAcheteur)
                .HasForeignKey(d => d.AcheteurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cnv_acheteurvnt");

            });

            modelBuilder.Entity<Couleur>(entity =>
            {
                entity.HasKey(e => e.CouleurId)
                .HasName("pk_clr");

            });

            modelBuilder.Entity<CouleurArticle>(entity =>
            {
                entity.HasKey(e => new { e.CouleurId, e.ArticleId})
                .HasName("pk_cla");

                entity.HasOne(d => d.ArticleConcerne)
                .WithMany(p => p.CouleursArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cla_art");

                entity.HasOne(d => d.CouleurConcernee)
                .WithMany(p => p.CouleursDesArticles)
                .HasForeignKey(d => d.CouleurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cla_clr");

            });

            modelBuilder.Entity<EtatArticle>(entity =>
            {
                entity.HasKey(e => e.EtatArticleId)
                .HasName("pk_eta");

            });

            modelBuilder.Entity<EtatVente>(entity =>
            {
                entity.HasKey(e => e.EtatVenteArticleId)
                .HasName("pk_eva");

            });

            modelBuilder.Entity<Expediteur>(entity =>
            {
                entity.HasKey(e => e.ExpediteurId)
                .HasName("pk_exp");

            });

            modelBuilder.Entity<Favoris>(entity =>
            {
                entity.HasKey(e => new { e.VintieId, e.ArticleId })
                .HasName("pk_fav");

                entity.HasOne(d => d.EstFavoris)
                .WithMany(p => p.FavorisArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_fav_art");

                entity.HasOne(d => d.FavorisVintie)
                .WithMany(p => p.FavorisDeVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_fav_vnt");

            });

            modelBuilder.Entity<FormatColis>(entity =>
            {
                entity.HasKey(e => e.Code)
                .HasName("pk_fmc");

            });

            modelBuilder.Entity<Horaire>(entity =>
            {
                entity.HasKey(e => new { e.PointRelaisID, e.JourId })
                .HasName("pk_hor");

                entity.HasOne(d => d.ACommeHoraire)
                .WithMany(p => p.HorairesPointRelais)
                .HasForeignKey(d => d.PointRelaisID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hor_ptr");

                entity.HasOne(d => d.JourOuvert)
                .WithMany(p => p.HeuresOuverts)
                .HasForeignKey(d => d.JourId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_hor_jor");

            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.HasKey(e => e.ImageId)
                .HasName("pk_img");

                entity.HasOne(d => d.ArticleDeImage)
                .WithMany(p => p.ImagesDeArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_img_art");
            });

            modelBuilder.Entity<Jour>(entity =>
            {
                entity.HasKey(e => e.JourId)
                .HasName("pk_jor");

            });

            modelBuilder.Entity<Marque>(entity =>
            {
                entity.HasKey(e => e.MarqueId)
                .HasName("pk_mrq");

            });

            modelBuilder.Entity<Matiere>(entity =>
            {
                entity.HasKey(e => e.MatiereId)
                .HasName("pk_mat");

            });

            modelBuilder.Entity<MatiereArticle>(entity =>
            {
                entity.HasKey(e => new { e.MatiereId, e.ArticleId })
                .HasName("pk_mar");

                entity.HasOne(d => d.MatiereDeArticle)
                .WithMany(p => p.MatieresDesArticles)
                .HasForeignKey(d => d.MatiereId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mar_mat");

                entity.HasOne(d => d.ArticleMatiere)
                .WithMany(p => p.ArticlesMatieres)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_mar_art");

            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.HasKey(e => e.MessageId)
                .HasName("pk_msg");

                entity.Property(e => e.DateEnvoi).HasDefaultValueSql("now()");

                entity.HasOne(d => d.ConversationMessage)
                .WithMany(p => p.Messages)
                .HasForeignKey(d => d.ConversationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_msg_cnv");

            });

            modelBuilder.Entity<MotifSignalement>(entity =>
            {
                entity.HasKey(e => e.MotifSignalementId)
                .HasName("pk_mos");

            });

            modelBuilder.Entity<Offre>(entity =>
            {
                entity.HasOne(d => d.EstStatusOffre)
                .WithMany(p => p.StatusOffres)
                .HasForeignKey(d => d.StatusOffreId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ofr_sto");

                entity.Property(e => e.StatusOffreId).HasDefaultValue(1);
            });

            modelBuilder.Entity<Pays>(entity =>
            {
                entity.HasKey(e => e.PaysId)
                .HasName("pk_pay");

            });

            modelBuilder.Entity<PointRelais>(entity =>
            {
                entity.HasKey(e => e.PointRelaisID)
                .HasName("pk_ptr");

                entity.HasOne(d => d.AdressePointRelais)
                .WithMany(p => p.ADesPointRelais)
                .HasForeignKey(d => d.AdresseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ptr_adr");

            });

            modelBuilder.Entity<PointRelaisFavoris>(entity =>
            {
                entity.HasKey(e => new { e.PointRelaisId, e.VintieId })
                .HasName("pk_prf");

                entity.HasOne(d => d.FavPointRelais)
                .WithMany(p => p.PointsRelaisEnFavoris)
                .HasForeignKey(d => d.PointRelaisId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_prf_ptr");

                entity.HasOne(d => d.VintiePointRelais)
                .WithMany(p => p.PointRelaisFavorisVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_prf_vnt");

            });

            modelBuilder.Entity<Possede>(entity =>
            {
                entity.HasKey(e => new { e.CodeType, e.AdresseId })
                .HasName("pk_psd");

                entity.HasOne(d => d.APourAdresse)
                .WithMany(p => p.PossedesAdresse)
                .HasForeignKey(d => d.AdresseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_psd_adr");

                entity.HasOne(d => d.APourType)
                .WithMany(p => p.PossedesType)
                .HasForeignKey(d => d.CodeType)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_psd_tad");

            });

            modelBuilder.Entity<Preference>(entity =>
            {
                entity.HasKey(e => new { e.ExpediteurId, e.VintieId })
                .HasName("pk_pre");

                entity.HasOne(d => d.VintieIdNavigation)
                .WithMany(p => p.PreferencesVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pre_vnt");

                entity.HasOne(d => d.ExpediteurIdNavigation)
                .WithMany(p => p.PreferencesExpediteur)
                .HasForeignKey(d => d.ExpediteurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_pre_exp");

            });

            modelBuilder.Entity<Reside>(entity =>
            {
                entity.HasKey(e => new { e.AdresseId, e.VintieId })
                .HasName("pk_rsd");

                entity.HasOne(d => d.ResideA)
                .WithMany(p => p.AResidents)
                .HasForeignKey(d => d.AdresseId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_rsd_adr");

                entity.HasOne(d => d.ResideVintie)
                .WithMany(p => p.VintiesResides)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_rsd_vnt");

            });

            modelBuilder.Entity<Retour>(entity =>
            {
                entity.HasKey(e => e.RetourId)
                .HasName("pk_ret");

                entity.Property(e => e.DateDemande).HasDefaultValueSql("now()");

                entity.HasOne(d => d.TypeDuRetour)
                .WithMany(p => p.TypesDesRetours)
                .HasForeignKey(d => d.TypeRetourId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ret_tpr");

                entity.HasOne(d => d.StatusDuRetour)
                .WithMany(p => p.StatusDesRetours)
                .HasForeignKey(d => d.StatusRetourId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ret_str");

                entity.HasOne(d => d.ArticleRetourne)
                .WithMany(p => p.RetourDesArticles)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ret_art");

                entity.HasOne(d => d.VintieRetour)
                .WithMany(p => p.RetourDesVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_ret_vnt");

            });

            modelBuilder.Entity<Signalement>(entity =>
            {
                entity.HasKey(e => e.SignalementId)
                .HasName("pk_sgn");

                entity.HasOne(d => d.ArticleDuSignalement)
                .WithMany(p => p.SignalementsDeArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sgn_art");

                entity.HasOne(d => d.VintieSignalant)
                .WithMany(p => p.SignalementsDeArticle)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sgn_vnt");

                entity.HasOne(d => d.StatusDuSignalement)
                .WithMany(p => p.StatusDesSignalements)
                .HasForeignKey(d => d.StatusSignalementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sgn_sts");

                entity.HasOne(d => d.MotifDuSignalement)
                .WithMany(p => p.MotifsDesSignalement)
                .HasForeignKey(d => d.MotifSignalementId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_sgn_mos");

                entity.Property(e => e.DateOuvertureTicket).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<StatusOffre>(entity =>
            {
                entity.HasKey(e => e.StatusOffreId)
                .HasName("pk_sto");

            });

            modelBuilder.Entity<StatusRetour>(entity =>
            {
                entity.HasKey(e => e.StatusRetourId)
                .HasName("pk_str");

            });

            modelBuilder.Entity<StatusSignalement>(entity =>
            {
                entity.HasKey(e => e.StatusSignalementId)
                .HasName("pk_sts");

            });

            modelBuilder.Entity<Taille>(entity =>
            {
                entity.HasKey(e => e.TailleId)
                .HasName("pk_tal");

                entity.HasOne(d => d.TypeTailleIdNavigation)
                .WithMany(p => p.TaillesTypeTaille)
                .HasForeignKey(d => d.TypeTailleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tal_tta");

            });

            modelBuilder.Entity<TailleArticle>(entity =>
            {
                entity.HasKey(e => new { e.TailleId, e.ArticleId })
                .HasName("pk_tar");

                entity.HasOne(d => d.ArticleIdNavigation)
                .WithMany(p => p.TaillesArticle)
                .HasForeignKey(d => d.ArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tar_art");

                entity.HasOne(d => d.TailleIdNavigation)
                .WithMany(p => p.ArticlesTaille)
                .HasForeignKey(d => d.TailleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tar_tal");

            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.HasKey(e => e.TransactionID)
                .HasName("pk_tsc");

                entity.Property(e => e.DateTransaction).HasDefaultValueSql("now()");

                entity.HasOne(d => d.CommandeTransaction)
                .WithMany(p => p.TransactionsCommandes)
                .HasForeignKey(d => d.CommandeID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_tsc_cmd");

            });

            modelBuilder.Entity<TypeAdresse>(entity =>
            {
                entity.HasKey(e => e.TypeAdresseId)
                .HasName("pk_tad");

            });

            modelBuilder.Entity<TypeAvis>(entity =>
            {
                entity.HasKey(e => e.TypeAvisID)
                .HasName("pk_tas");

            });

            modelBuilder.Entity<TypeCompte>(entity =>
            {
                entity.HasKey(e => e.TypeCompteId)
                .HasName("pk_tyc");

            });

            modelBuilder.Entity<TypeEnvoi>(entity =>
            {
                entity.HasKey(e => e.TypeEnvoiId)
                .HasName("pk_tye");

            });

            modelBuilder.Entity<TypeRetour>(entity =>
            {
                entity.HasKey(e => e.TypeRetourId)
                .HasName("pk_tpr");

            });

            modelBuilder.Entity<TypeTaille>(entity =>
            {
                entity.HasKey(e => e.TypeTailleId)
                .HasName("pk_tta");

            });

            modelBuilder.Entity<Ville>(entity =>
            {
                entity.HasKey(e => e.VilleId)
                .HasName("pk_vil");

                entity.HasOne(d => d.PaysVille)
                .WithMany(p => p.VillesPays)
                .HasForeignKey(d => d.PaysId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_vil_pay");

            });

            modelBuilder.Entity<Vintie>(entity =>
            {
                entity.HasKey(e => e.VintieId)
                .HasName("pk_vnt");

                entity.Property(e => e.DateInscription).HasDefaultValueSql("now()");
                entity.Property(e => e.DateDerniereConnexion).HasDefaultValueSql("now()");

                entity.HasOne(d => d.VintieCodeNavigation)
                .WithMany(p => p.VintiesType)
                .HasForeignKey(d => d.TypeCompteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_vnt_tyc");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
