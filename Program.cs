using HeroesWeb.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString =
    builder.Configuration.GetConnectionString("HeroesDb")
    ?? throw new InvalidOperationException(
        "No se encontró la conexión HeroesDb.");

builder.Services.AddRazorPages(options =>
{
    options.Conventions.AuthorizeFolder("/Heroes");
    options.Conventions.AuthorizeFolder("/SuperPoderes");
});

// Soporte MVC (controladores y vistas) junto con Razor Pages.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<HeroesContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services
    .AddDefaultIdentity<IdentityUser>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = true;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
    .WithStaticAssets();

// Ruta convencional MVC sin controlador por defecto, para no chocar con
// la página de inicio de Razor Pages. Ej.: /LaboratorioEf/Editar/1
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();
