using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.DataManager;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IArticleRepository<Article>, ArticleManager>();
builder.Services.AddScoped<IVintieRepository<Vintie>, VintieManager>();
builder.Services.AddScoped<ICommandeRepository<Commande>, CommandeManager>();
builder.Services.AddScoped<IDataRepository<Retour>, RetourManager>();

builder.Services.AddDbContext<VintedDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VintedDBContext")));

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
