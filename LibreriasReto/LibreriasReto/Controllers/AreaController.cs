using LibreriasReto.BLL.Servicios.Contrato;
using Microsoft.AspNetCore.Mvc;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace LibreriasReto.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrador")]
    public class AreaController : Controller
    {

        private readonly IAreaService _service;

        public AreaController(IAreaService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            return View(await _service.Listar() );
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new AreaDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(AreaDTO area)
        {
            bool respuesta = await _service.Registrar(area);
            if (respuesta) return RedirectToAction("Lista");
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            List<AreaDTO> listado = await _service.Listar();
            var areaEncontrada = listado.FirstOrDefault(i => i.IdArea == id);
            return View(areaEncontrada);
        }

        [HttpPost]
        public async Task<IActionResult> Update(AreaDTO area)
        {
            bool respuesta = await _service.Actualizar(area);
            if (respuesta) return RedirectToAction("Lista");
            return View();

        }

    }
}
