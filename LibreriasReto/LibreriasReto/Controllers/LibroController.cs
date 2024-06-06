using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriasReto.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _servicio;

        public LibroController(ILibroService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            return View( await _servicio.Listar());
        }

        [HttpPost]
        public IActionResult Buscar(int id)
        {
            return View(_servicio.Buscar(id));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View(new LibroDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(LibroDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return RedirectToAction("Listar");
            ViewBag.mensaje = "No se pudo registrar el libro";
            return RedirectToAction("Registrar");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            return View( await _servicio.Buscar(id));
        }

        [HttpPost]
        public async Task<IActionResult> Update(LibroDTO cliente)
        {
            await _servicio.Actualizar(cliente);
            return RedirectToAction("listar");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            LibroDTO cliente = await _servicio.Buscar(id);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(LibroDTO libro)
        {
            await _servicio.Eliminar(libro.IdLibro);
            return RedirectToAction(nameof(Lista));
        }
    }
}
