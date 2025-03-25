using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SAE401_API_Vinted.Models;
using SAE401_API_Vinted.Models.DataManager;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
             .AddJwtBearer(options =>
             {
                 options.RequireHttpsMetadata = false;
                 options.SaveToken = true;
                 options.TokenValidationParameters = new TokenValidationParameters
                 {
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ValidateLifetime = true,
                     ValidateIssuerSigningKey = true,
                     ValidIssuer = builder.Configuration["Jwt:Issuer"],
                     ValidAudience = builder.Configuration["Jwt:Audience"],
                     IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"])),
                     ClockSkew = TimeSpan.Zero
                 };
             });

            builder.Services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });
builder.Services.AddScoped<IArticleRepository<Article>, ArticleManager>();
builder.Services.AddScoped<IVintieRepository<Vintie>, VintieManager>();
builder.Services.AddScoped<ICommandeRepository<Commande>, CommandeManager>();
builder.Services.AddScoped<IDataRepository<Retour>, RetourManager>();
builder.Services.AddScoped<IDataRepository<Conversation>, ConversationManager>();
builder.Services.AddScoped<IDataRepository<Message>, MessageManager>();
builder.Services.AddScoped<IDataRepository<Offre>, OffreManager>();
builder.Services.AddScoped<IAvisRepository<Avis>, AvisManager>();
builder.Services.AddScoped<IGetDataRepository<Categorie>, CategorieManager>();
builder.Services.AddScoped<IDataRepository<Image>, ImageManager>();
builder.Services.AddScoped<IDataRepository<Adresse>, AdresseManager>();
builder.Services.AddScoped<IPointRelaisRepository<PointRelais>, PointRelaisManager>();
builder.Services.AddScoped<IJointureRepository<Possede>, PossedeManager>();
builder.Services.AddScoped<IGetDataRepository<TypeTaille>, TypeTailleManager>();
builder.Services.AddScoped<IJointureRepository<Reside>, ResideManager>();
builder.Services.AddScoped<IJointureRepository<Appartient>, AppartientManager>();
builder.Services.AddScoped<IJointureRepository<Preference>, PreferenceManager>();
builder.Services.AddScoped<IJointureRepository<TailleArticle>, TailleArticleManager>();
builder.Services.AddScoped<IJointureRepository<Favoris>,  FavorisManager>();
builder.Services.AddScoped<IJointureRepository<MatiereArticle>, MatiereArticleManager>();
builder.Services.AddScoped<IJointureRepository<CouleurArticle>, CouleurArticleManager>();
builder.Services.AddScoped<IJointureRepository<PointRelaisFavoris>, PointRelaisFavorisManager>();

builder.Services.AddDbContext<VintedDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("AzureVintedDBContext")));

builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5174")  
              .AllowAnyMethod()                    
              .AllowAnyHeader();                   
    });
});

var app = builder.Build();

app.UseCors("AllowLocalhost");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
