using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibreriasReto.Controllers
{
    public class AccesoController : Controller
    {

        private readonly IAccesoService _servicio;

        public AccesoController(IAccesoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult login()
        {
            return View(new AccesoDTO());
        }

        [HttpPost]
        public async Task<IActionResult> loginIngresar(int clave, string password)
        {
            AccesoDTO logueo = await _servicio.Logueo(clave, password);
            if (logueo == null)
            {
                ViewBag.MENSAJE = "Dni o contraceña incorrecta";
                return View("login");
            }
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, logueo.EmpleadoNombre),
                new Claim(ClaimTypes.Role, logueo.EmpleadoRol),
                new Claim("idEmpleado", logueo.IdEmpleado.ToString()),
                new Claim("rolEmpleado", logueo.EmpleadoRol)
            };
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("login", "Acceso");
        }

    }
}
