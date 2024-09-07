using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace LibreriasReto.Controllers
{
    public class ComprobanteController : Controller
    {

        private readonly IComprobanteServices _comprobanteServices;
        private readonly IMetodoPagoService _metodoPagoService;
        private readonly ILibroService _libroService;
        private readonly IVentaService _ventaService;
        private readonly IClienteServices _clienteServices;

        public ComprobanteController(IComprobanteServices comprobanteServices, IClienteServices clienteServices, IMetodoPagoService metodoPagoService, IVentaService ventaService, ILibroService libroService)
        {
            _comprobanteServices = comprobanteServices;
            _clienteServices = clienteServices;
            _metodoPagoService = metodoPagoService;
            _ventaService = ventaService;
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
        public async Task<IActionResult> Reportes(string empleado = null, string fechaInicio = null, string fechaFin = null)
        {
            List<ComprobanteDTO> listadoVentas = await _comprobanteServices.Listar();
            if (empleado != null) listadoVentas = await _comprobanteServices.Listar(u => u.IdEmpleadoNavigation.Nombre.Contains(empleado));
            if (fechaInicio != null && fechaFin != null)
            {
                listadoVentas = await _comprobanteServices.Listar();
                if (empleado != null) listadoVentas = await _comprobanteServices.Listar(u => u.IdEmpleadoNavigation.Nombre.Contains(empleado));
                DateTime _fechaInicio = Convert.ToDateTime(fechaInicio, new CultureInfo("es-PE"));
                DateTime _fechaFin = Convert.ToDateTime(fechaFin, new CultureInfo("es-PE"));
                listadoVentas = listadoVentas.Where(v => Convert.ToDateTime(v.FechaVenta) >= _fechaInicio && Convert.ToDateTime(v.FechaVenta) <= _fechaFin).ToList();
            }
            return View(listadoVentas);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            ComprobanteDTO comprobanteEncontrado = await _comprobanteServices.Buscar(id);
            List<VentaDTO> ventaEncontrada = await _ventaService.Listar(id);
            DetalleVentaViewModel detalleVenta = new DetalleVentaViewModel
            {
                Comprobante = comprobanteEncontrado,
                Venta = ventaEncontrada
            };
            return View(detalleVenta);
        }

    }
}
