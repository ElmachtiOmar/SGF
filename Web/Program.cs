using Abp.Domain.Uow;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var CarRentalDb = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'CarRentalDb' not found.");
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(CarRentalDb));


builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

// Register the unit of work
builder.Services.AddScoped<IUnitOfWork<Client>, UnitOfWork<Client>>();
builder.Services.AddScoped<IUnitOfWork<Facture>, UnitOfWork<Facture>>();
builder.Services.AddScoped<IUnitOfWork<Produit>, UnitOfWork<Produit>>();
builder.Services.AddScoped<IUnitOfWork<LigneFacture>, UnitOfWork<LigneFacture>>();
builder.Services.AddScoped<IUnitOfWork<Payment>, UnitOfWork<Payment>>();

var app = builder.Build();



// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Factures}/{action=Index}/{id?}");

app.Run();
