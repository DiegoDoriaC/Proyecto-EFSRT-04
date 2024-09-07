using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Globalization;

namespace LibreriasReto.Controllers
{
    [Authorize]
    [Authorize(Roles = "Administrador")]
    public class RecepcionController : Controller
    {
        private readonly IRecepcionService _servicio;
        private readonly ILibroService _libro;

        public RecepcionController(IRecepcionService servicio, ILibroService libro)
        {
            _servicio = servicio;
            _libro = libro;
        }

        [HttpGet]
        public async Task<IActionResult> Lista(int pagina = 1)
        {
            List<RecepcionDTO> listaRecepciones = await _servicio.Listar();
            int cantidadRegistrosPorPagina = 10;
            var librosParaLaPaginacion = listaRecepciones.Skip((pagina - 1) * cantidadRegistrosPorPagina).Take(cantidadRegistrosPorPagina).ToList();
            var totalDeRegistros = listaRecepciones.Count();
            //CODIGO PARA LA PAGINACION            
            var modelo = new PaginacionModelo<RecepcionDTO>();
            modelo.listaGenerica = librosParaLaPaginacion;
            modelo.PaginaActual = pagina;
            modelo.TotalDeRegistros = totalDeRegistros;
            modelo.RegistrosPorPagina = cantidadRegistrosPorPagina;

            ViewBag.MENSAJE = null;
            ViewBag.ReporteSemanal = 0;
            ViewBag.ReporteMensual = 0;
            ViewBag.ReporteTotal = 0;
            if (listaRecepciones.Count == 0)
            {
                ViewBag.MENSAJE = "Ningun registro encontrado...";
                return View(modelo);
            }

            var fechaSemana = DateTime.Now.AddDays(-7);
            var fechaMes = DateTime.Now.AddMonths(-1);

            ViewBag.ReporteSemanal = listaRecepciones.Where(r => Convert.ToDateTime(r.FechaIngreso, new CultureInfo("es-PE")) > fechaSemana).Count();
            ViewBag.ReporteMensual = listaRecepciones.Where(r => Convert.ToDateTime(r.FechaIngreso, new CultureInfo("es-PE")) > fechaMes).Count();

            ViewBag.ReporteTotal = listaRecepciones.ToList().Count();
            return View(modelo);
        }

        [HttpGet]
        public IActionResult Filter()
        {
            return View(new List<RecepcionDTO>());
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string nombre = "", string fechaInicio = "", string fechaFin = "")
        {
            List<RecepcionDTO> listaRecepciones = await _servicio.Listar(nombre, fechaInicio, fechaFin);
            ViewBag.MENSAJE = null;
            ViewBag.ReporteSemanal = 0;
            ViewBag.ReporteMensual = 0;
            ViewBag.ReporteTotal = 0;
            if (listaRecepciones.Count == 0)
            {
                ViewBag.MENSAJE = "Ningun registro encontrado...";
                return View(listaRecepciones);
            }
            ViewBag.ReporteSemanal = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) >= DateTime.Now.AddDays(-7)).Count();
            ViewBag.ReporteMensual = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) >= DateTime.Now.AddMonths(-1)).Count();
            ViewBag.ReporteTotal = listaRecepciones.ToList().Count();
            return View(listaRecepciones);
        }

        [HttpPost]
        public async Task<IActionResult> Search(int id)
        {
            return View(await _servicio.Buscar(id));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.LIBROS = new SelectList(await _libro.Listar(), "IdLibro", "Nombre");
            return View(new RecepcionDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(RecepcionDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return RedirectToAction("Lista");
            ViewBag.mensaje = "No se pudo registrar la recepcion";
            return RedirectToAction("Registrar");
        }
    }
}
