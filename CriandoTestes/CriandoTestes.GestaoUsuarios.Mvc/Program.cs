using CriandoTestes.GestaoUsuarios.Configuracoes.Extensoes;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace CriandoTestes.GestaoUsuarios.Mvc;

public class Program
{
  public static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
    builder.Services.AddHttpContextAccessor();
    builder
      .Services
      .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
      .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

    // Gestão de usuários
    builder.Services.AddGestaoUsuarios(builder.Configuration);

    var app = builder.Build();
    app.UseDeveloperExceptionPage();

    app.UseStaticFiles();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
  }
}
