using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;

namespace LibreriasReto.Controllers
{
    public class ClienteController : Controller
    {

        private readonly IClienteServices _servicio;

        public ClienteController(IClienteServices servicio)
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
        public async Task<IActionResult> Create(ClienteDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            //ViewBag.MENSAJE = "s";
            if (respuesta) return RedirectToAction(nameof(Lista));
            ViewBag.MENSAJE = "No se pudo registrar el cliente";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            ClienteDTO cliente = await _servicio.Buscar(id);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ClienteDTO cliente)
        {
            bool respuesta =  await _servicio.Actualizar(cliente);
            return RedirectToAction("lista");
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            ClienteDTO cliente = await _servicio.Buscar(id);
            return View(cliente);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(ClienteDTO cliente)
        {
            bool respuesta = await _servicio.Eliminar(cliente.IdCliente);
            return RedirectToAction(nameof(Lista));
        }


    }
}
