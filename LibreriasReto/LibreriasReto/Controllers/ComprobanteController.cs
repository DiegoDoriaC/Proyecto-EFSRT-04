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
            //ViewBag.Libros = new SelectList(await _libroService.Listar(), "IdLibro", "Nombre");
            ViewBag.MetodoPago = new SelectList(await _metodoPagoService.listar(), "IdMetodoPago", "Nombre");
            return View(new ComprobanteDTO());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ComprobanteDTO comprobante)
        {
            List<VentaDTO> ventaRegitrada = comprobante.Venta;
            ViewBag.RegistrarVenta = "No se pudo registrar la venta";
            bool response = await _comprobanteServices.Registrar(comprobante);
            if (response) ViewBag.RegistrarVenta = "Venta registrada satisfactoriamente";
            return View();
        }
    }
}
