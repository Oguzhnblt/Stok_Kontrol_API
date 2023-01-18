using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Stok_Kontrol_API.Repositories.Concrete;
using Stok_Kontrol_API.Repositories.Context;
using Stok_Kontrol_API.Service.Abstract;
using Stok_Kontrol_API.Service.Concrete;
using StokKontrolProject.Repositories.Abstract;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson(option =>
{
    option.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StockControlContext>(option =>
{
    option.UseSqlServer("Server=OGUZ; Database=StockControl; uid=sa; pwd=123;");
});


builder.Services.AddTransient(typeof(IGenericService<>), typeof(GenericManager<>));
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
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
