using System.Diagnostics;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LibreriasReto.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            AccesoDTO empleadoGenerico = new AccesoDTO();
            ViewBag.EmpleadoJson = null;
            //if (TempData["usuarioId"] != null) 
            //{ 
            //    empleadoGenerico.IdEmpleado = (int)TempData["usuarioId"];
            //    empleadoGenerico.EmpleadoNombre = TempData["usuarioNombre"] as string;
            //    string empleadoJson = JsonConvert.SerializeObject(empleadoGenerico);
            //    // Pasar el JSON a la vista
            //    ViewBag.EmpleadoJson = empleadoJson;            
            //}
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
