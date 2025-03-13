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
            optionsBuilder.UseNpgsql("Host=localhost; Database=VintedDB; Username=postgres; Password=3246");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Adresse>(entity =>
            {
               entity.HasKey(e => e.AdresseID)
                .HasName("pk_adresse");

                entity.HasOne(d => d.VilleAdresse)
                .WithMany(p => p.AdressesVilles)
                .HasForeignKey(d => d.VilleID)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_adresse_ville");
            });

            modelBuilder.Entity<Appartient>(entity =>
            {
                entity.HasKey(e => new { e.CompteId, e.VintieId })
                 .HasName("pk_appartient");

                entity.HasOne(d => d.CompteIdNavigation)
                .WithMany(p => p.AppartientCompte)
                .HasForeignKey(d => d.CompteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_appartient_compte");

                entity.HasOne(d => d.VintieIdNavigation)
                .WithMany(p => p.AppartienentVintie)
                .HasForeignKey(d => d.VintieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_appartient_vintie");
            });


            modelBuilder.Entity<Article>(entity =>
            {
                entity.HasKey(e => e.ArticleId)
                .HasName("pk_article");
                entity.Property(e => e.DateAjout).HasDefaultValueSql("now()");

                entity.HasOne(d => d.EtatDeArticle)
                .WithMany(p => p.EtatsDesArticles)
                .HasForeignKey(d => d.EtatArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_etatarticle");

                entity.HasOne(d => d.VendeurDeArticle)
                .WithMany(p => p.ArticlesDuVendeur)
                .HasForeignKey(d => d.VendeurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_vinties");

                entity.HasOne(d => d.MarqueDeArticle)
                .WithMany(p => p.MarquesDesArticles)
                .HasForeignKey(d => d.MarqueId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_marque");

                entity.HasOne(d => d.EtatVenteDeArticle)
                .WithMany(p => p.EtatsVenteDesArticles)
                .HasForeignKey(d => d.EtatVenteArticleId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_etatvente");

                entity.HasOne(d => d.CategorieDeArticle)
                .WithMany(p => p.CategoriesArticles)
                .HasForeignKey(d => d.CategorieId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_article_categorie");

            });

            modelBuilder.Entity<Avis>(entity =>
            {
                entity.HasKey(e => e.AvisId)
                 .HasName("pk_avis");

                entity.HasOne(d => d.APourTypeAvis)
                .WithMany(p => p.PossedesTypeAvis)
                .HasForeignKey(d => d.CodeTypeAvis)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avis_typeavis");

                entity.HasOne(d => d.APourVendeur)
                .WithMany(p => p.ADesAvisVendeur)
                .HasForeignKey(d => d.VendeurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avis_vendeurvinties");

                entity.HasOne(d => d.APourAcheteur)
                .WithMany(p => p.ADesAvisAcheteur)
                .HasForeignKey(d => d.AcheteurId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_avis_acheteurvinties");
            });

            modelBuilder.Entity<CarteBancaire>(entity =>
            {
                entity.HasKey(e => e.CarteId)
                 .HasName("pk_cartebancaire");

                entity.HasOne(d => d.CompteIdNavigation)
                .WithMany(p => p.CartesCompte)
                .HasForeignKey(d => d.CompteId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_cartebancaire_comptebancaire");
            });

            modelBuilder.Entity<Categorie>(entity =>
            {
                entity.HasKey(e => e.CategorieId)
                 .HasName("pk_categorie");

                entity.HasOne(d => d.CategorieParentIdNavigation)
                .WithMany(p => p.CategoriesParent)
                .HasForeignKey(d => d.IdParent)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_categorie_categorie");
            });


            modelBuilder.Entity<Commande>(entity =>
            {
                entity.HasKey(e => e.CommandeID)
                 .HasName("pk_commande");

                entity.HasOne(d => d.ACommeFormat)
                .WithMany(p => p.ADesCommandes)
                .HasForeignKey(d => d.CodeFormat)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("fk_commande_formatcolis");
            });

            modelBuilder.Entity<Message>(entity =>
            {

                entity.Property(e => e.DateEnvoi).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Retour>(entity =>
            {

                entity.Property(e => e.DateDemande).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Signalement>(entity =>
            {

                entity.Property(e => e.DateOuvertureTicket).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Transaction>(entity =>
            {

                entity.Property(e => e.DateTransaction).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Vintie>(entity =>
            {

                entity.Property(e => e.DateInscription).HasDefaultValueSql("now()");
                entity.Property(e => e.DateDerniereConnexion).HasDefaultValueSql("now()");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
