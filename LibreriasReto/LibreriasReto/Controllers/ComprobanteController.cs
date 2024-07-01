using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriasReto.Controllers
{
    public class ComprobanteController : Controller
    {

        private readonly IComprobanteServices _comprobanteServices;
        private readonly IMetodoPagoService _metodoPagoService;
        private readonly ILibroService _libroService;
        private readonly IClienteServices _clienteServices;

        public ComprobanteController(IComprobanteServices comprobanteServices, IClienteServices clienteServices, IMetodoPagoService metodoPagoService, ILibroService libroService)
        {
            _comprobanteServices = comprobanteServices;
            _clienteServices = clienteServices;
            _metodoPagoService = metodoPagoService;
            _libroService = libroService;
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Clientes = await _clienteServices.Listar();
            ViewBag.Libros = await _libroService.Listar();
            ViewBag.MetodoPago = new SelectList(await _metodoPagoService.listar(), "IdMetodoPago", "Nombre");
            return View(new ComprobanteDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComprobanteDTO comprobante)
        {
            bool response = await _comprobanteServices.Registrar(comprobante);
            var result = new
            {
                success = response,
                message = response ? "Venta registrada satisfactoriamente" : "No se pudo registrar la venta, compruebe el stock"
            };
            return Json(result);          
        }

        [HttpGet]
        public IActionResult Reportes()
        {
            return View(new List<ComprobanteDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Reportes(int pagina = 1, string empleado = null)
        {
            List<ComprobanteDTO> listadoVentas = null;            
            if(empleado != null) listadoVentas = await _comprobanteServices.Listar(u => u.IdEmpleadoNavigation.Nombre.Contains(empleado));
          
            return View(listadoVentas);
        }

    }
}
