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
// Tokens.
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
// IDataRepository.
builder.Services.AddScoped<IDataRepository<Retour>, RetourManager>();
builder.Services.AddScoped<IDataRepository<Image>, ImageManager>();
// IGetDataRepository.
builder.Services.AddScoped<IGetDataRepository<Expediteur>, ExpediteurManager>();
builder.Services.AddScoped<IGetDataRepository<TypeTaille>, TypeTailleManager>();
// IModelRepository.
builder.Services.AddScoped<IAdresseRepository, AdresseManager>();
builder.Services.AddScoped<IArticleRepository, ArticleManager>();
builder.Services.AddScoped<IConversationRepository, ConversationManager>();
builder.Services.AddScoped<IVintieRepository, VintieManager>();
builder.Services.AddScoped<ICommandeRepository, CommandeManager>();
builder.Services.AddScoped<IAvisRepository, AvisManager>();
builder.Services.AddScoped<ICategorieRepository, CategorieManager>();
builder.Services.AddScoped<IPointRelaisRepository, PointRelaisManager>();
// IJointureRepository.
builder.Services.AddScoped<IJointureRepository<Possede>, PossedeManager>();
builder.Services.AddScoped<IJointureRepository<Reside>, ResideManager>();
builder.Services.AddScoped<IJointureRepository<Appartient>, AppartientManager>();
builder.Services.AddScoped<IJointureRepository<Preference>, PreferenceManager>();
builder.Services.AddScoped<IJointureRepository<TailleArticle>, TailleArticleManager>();
builder.Services.AddScoped<IJointureRepository<Favoris>,  FavorisManager>();
builder.Services.AddScoped<IJointureRepository<MatiereArticle>, MatiereArticleManager>();
builder.Services.AddScoped<IJointureRepository<CouleurArticle>, CouleurArticleManager>();
builder.Services.AddScoped<IJointureRepository<PointRelaisFavoris>, PointRelaisFavorisManager>();
// DbContext.
builder.Services.AddDbContext<VintedDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VintedDBContext")));
// Controllers.
builder.Services.AddControllers();
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(
    doc =>
    {
        // Configure Swagger to use the XML documentation file.
        var xmlFile = Path.ChangeExtension(typeof(Program).Assembly.Location, ".xml");
        doc.IncludeXmlComments(xmlFile);
    }
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost", policy =>
    {
        policy.WithOrigins("http://localhost:5177")  
              .AllowAnyMethod()                    
              .AllowAnyHeader();                   
    });
});

var app = builder.Build();

app.UseCors(policy =>
    policy.WithOrigins("https://sae401vinted-gmfsa3e7d8bwa8g6.francecentral-01.azurewebsites.net")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials()
);

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
