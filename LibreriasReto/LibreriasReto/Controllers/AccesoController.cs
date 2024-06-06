using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;

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
            return RedirectToAction("Index", "Home");
        }

    }
}
