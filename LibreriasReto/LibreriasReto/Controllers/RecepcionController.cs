using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriasReto.Controllers
{
    public class RecepcionController : Controller
    {
        private readonly IRecepcionService _servicio;

        public RecepcionController(IRecepcionService servicio, IRecepcionRepository repository)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public IActionResult Listar()
        {
            return View(_servicio.Listar());
        }

        [HttpPost]
        public IActionResult Buscar(int id)
        {
            return View(_servicio.Buscar(id));
        }


        [HttpGet]
        public IActionResult Registrar()
        {
            return View(new RecepcionDTO());
        }


        //MUCHO CUIDADO
        [HttpPost]
        public async Task<IActionResult> Registrar(RecepcionDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return RedirectToAction("Listar");
            ViewBag.mensaje = "No se pudo registrar la recepcion";
            return RedirectToAction("Registrar");
        }
    }
}
