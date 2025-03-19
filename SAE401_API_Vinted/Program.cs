using Microsoft.EntityFrameworkCore;
using SAE401_API_Vinted.Models.DataManager;
using SAE401_API_Vinted.Models.EntityFramework;
using SAE401_API_Vinted.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IDataRepositoryArticleVintie<Article>, ArticleManager>();

builder.Services.AddDbContext<VintedDBContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("VintedDBContext")));

builder.Services.AddControllers();
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
