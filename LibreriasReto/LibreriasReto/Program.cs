using Microsoft.AspNetCore.Authentication.Cookies;
using LibreriasReto.IOC;
using System.Globalization;
using Microsoft.AspNetCore.Localization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.InyectarDependencias(builder.Configuration);
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(option =>
    {
        option.LoginPath = "/Acceso/Login";
        option.ExpireTimeSpan = TimeSpan.FromMinutes(20);
        //option.AccessDeniedPath = "/Acceso/Login";
    });
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
//app.UseAuthentication();

var supportedCultures = new[] { new CultureInfo("es-PE") };
var localizationOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("es-PE"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
};

app.UseRequestLocalization(localizationOptions);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Acceso}/{action=Login}");
//Ejecucion de la aplicacion
app.Run();
