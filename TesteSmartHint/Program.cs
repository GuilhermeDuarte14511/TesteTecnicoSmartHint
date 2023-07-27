using Business;
using Business.Interfaces;
using Entities.Db;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configure services
string connectionString = builder.Configuration.GetConnectionString("LojaDB");
builder.Services.AddDbContext<ITesteSmartHintContext, TesteSmartHintContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

// Add ClientesService service
builder.Services.AddScoped<IClientesService, ClientesService>();

builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
