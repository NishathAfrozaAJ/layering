using layering.Models;
using layering.Repository;
using layering.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Logging.AddLog4Net();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
builder.Services.AddTransient<IProductServ<Product>, ProductServ>();
builder.Services.AddTransient<IProductRepo<Product>, ProductRepo> ();
builder.Services.AddTransient<ICustomerServ<Customer>, CustomerServ>();
builder.Services.AddTransient<ICustomerRepo<Customer>, CustomerRepo>();
builder.Services.AddTransient<IPurServ<Pur>, PurServ>();
builder.Services.AddTransient<IPurRepocs<Pur>, PurRepo>();
builder.Services.AddTransient<IStockistServ<Stockist>, StockistServ>();
//builder.Services.AddSingleton<IStockistServ<Stockist>, StockistServ>();
builder.Services.AddTransient<IStockistRepo<Stockist>, StockistRepo>();

builder.Services.AddDbContext<stockContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(x=>x
        .AllowAnyOrigin()
        .AllowAnyMethod()
        .AllowAnyHeader()
        );
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
