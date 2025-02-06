using CriandoTestes.GestaoUsuarios.Mvc.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CriandoTestes.GestaoUsuarios.Mvc.Controllers;

public class AutenticacaoController : Controller
{
  [HttpGet]
  public IActionResult Login() => View();

  [HttpPost]
  public async Task<IActionResult> LoginAsync(AutenticacaoViewModel viewModel)
  {
    var claims = new List<Claim>
    {
        new Claim(ClaimTypes.Name, viewModel.Email),
        new Claim(ClaimTypes.Email, viewModel.Email)
    };

    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
    var authProperties = new AuthenticationProperties
    {
      IsPersistent = true,
      ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
    };

    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);
    return RedirectToAction("Index", "Home");
  }

  [HttpPost]
  public async Task<IActionResult> LogoutAsync()
  {
    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return RedirectToAction("Index", "Home");
  }
}
