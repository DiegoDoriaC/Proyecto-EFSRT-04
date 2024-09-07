using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LibreriasReto.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrador")]
    public class GeneroController : Controller
    {
        private readonly IGeneroService _service;

        public GeneroController(IGeneroService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            return View(await _service.Listar());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new GeneroDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(GeneroDTO genero)
        {
            bool respuesta = await _service.Registrar(genero);
            if (respuesta) return RedirectToAction("Lista");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            List<GeneroDTO> listado = await _service.Listar();
            var generoEncontrado = listado.FirstOrDefault(i => i.IdGenero == id);
            return View(generoEncontrado);
        }

        [HttpPost]
        public async Task<IActionResult> Update(GeneroDTO genero)
        {
            bool respuesta = await _service.Actualizar(genero);
            if (respuesta) return RedirectToAction("Lista");
            return View();
        }
    }
}
