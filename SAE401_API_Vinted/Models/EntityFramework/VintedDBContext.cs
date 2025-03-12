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

        public virtual DbSet<TailleArticle> TaillesArticles { get; set; }

        public virtual DbSet<Avis> Avis { get; set; }

        public virtual DbSet<CarteBancaire> CartesBancaires { get; set; }

        public virtual DbSet<Commande> Commandes { get; set; }

        public virtual DbSet<CompteBancaire> ComptesBancaires { get; set; }

        public virtual DbSet<Conversation> Conversations { get; set; }

        public virtual DbSet<EtatArticle> EtatsArticles { get; set; }

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

        public virtual DbSet<StatusRetour> StatusRetours { get; set; }
        public virtual DbSet<StatusSignalement> StatusSignalements { get; set; }

        public virtual DbSet<Taille> Tailles { get; set; }

        public virtual DbSet<Transaction> Transactions { get; set; }

        public virtual DbSet<TypeAdresse> TypesAdresses { get; set; }

        public virtual DbSet<TypeAvis> TypesAvis { get; set; }

        public virtual DbSet<TypeCompte> TypesComptes { get; set; }

        public virtual DbSet<TypeRetour> TypesRetours { get; set; }

        public virtual DbSet<TypeStatusOffre> TypesStatusOffres { get; set; }

        public virtual DbSet<TypeTaille> TypeTailles { get; set; }
                                                                                                                 
        public virtual DbSet<Ville> Villes { get; set; }

        public virtual DbSet<Vintie> Vinties { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=SAE401_API_Vinted;Username=postgres;Password=");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.HasDefaultSchema("td4");

            //modelBuilder.Entity<Adresse>(entity =>
            //{
            //    entity.HasKey(e => e.AdresseID)
            //    .HasName("pk_adresse");

            //    entity.HasOne(d => d.VilleAdresse)
            //    .WithMany(p => p.AdressesVilles)
            //    .HasForeignKey(d => d.VilleID)
            //    .OnDelete(DeleteBehavior.Restrict)
            //    .HasConstraintName("fk_adresse_ville");
            //});

            //modelBuilder.Entity<Appartient>(entity =>
            //{
            //    entity.HasKey(e => new { e.CompteId, e.VintieId})
            //        .HasName("pk_app");

            //    entity.HasOne(d => d.CompteIdNavigation)
            //        .WithMany(p => p.AppartientCompte)
            //        .HasForeignKey(d => d.FilmId)
            //        .OnDelete(DeleteBehavior.Restrict)
            //        .HasConstraintName("fk_notation_film");

            //    entity.HasOne(d => d.UtilisateurNotant)
            //        .WithMany(p => p.NotesUtilisateur)
            //        .HasForeignKey(d => d.UtilisateurId)
            //        .OnDelete(DeleteBehavior.Restrict)
            //        .HasConstraintName("fk_notation_utilisateur");

            //});

            modelBuilder.Entity<Vintie>(entity =>
            {

                entity.Property(e => e.DateInscription).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Message>(entity =>
            {

                entity.Property(e => e.MessageDateEnvoi).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Transaction>(entity =>
            {

                entity.Property(e => e.DateTransaction).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Article>(entity =>
            {

                entity.Property(e => e.DateAjout).HasDefaultValueSql("now()");

            });

            modelBuilder.Entity<Signalement>(entity =>
            {

                entity.Property(e => e.DateOuvertureTicket).HasDefaultValueSql("now()");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
