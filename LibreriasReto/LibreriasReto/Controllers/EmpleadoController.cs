using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;

namespace LibreriasReto.Controllers
{
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoService _servicio;

        public EmpleadoController(IEmpleadoService servicio)
        {
            _servicio = servicio;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var listado = await _servicio.Listar();
            return View(listado);
        }

        [HttpPost]
        public IActionResult Search(int id)
        {
            return View(_servicio.Buscar(id));
        }


        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpleadoDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return View();
            ViewBag.mensaje = "No se pudo registrar el empleado";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            EmpleadoDTO empleado = await _servicio.Buscar(id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Update(EmpleadoDTO empleado)
        {
            bool respuesta = await _servicio.Actualizar(empleado);
            return RedirectToAction("lista");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            EmpleadoDTO empleado = await _servicio.Buscar(id);
            return View(empleado);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EmpleadoDTO empleado)
        {
            bool respuesta = await _servicio.Eliminar(empleado.IdEmpleado);
            return RedirectToAction(nameof(Lista));
        }
    }
}
