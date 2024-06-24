using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriasReto.Controllers
{
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
        public async Task<IActionResult> Lista()
        {
            List<RecepcionDTO> listaRecepciones = await _servicio.Listar();
            ViewBag.MENSAJE = null;
            ViewBag.ReporteSemanal = 0;
            ViewBag.ReporteMensual = 0;
            ViewBag.ReporteTotal = 0;
            if (listaRecepciones.Count == 0)
            {
                ViewBag.MENSAJE = "Ningun registro encontrado...";
                return View(listaRecepciones);
            }
            var fechaSemana = DateTime.Now.AddDays(-7);
            ViewBag.ReporteSemanal = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) > DateTime.Now.AddDays(-7)).Count();
            ViewBag.ReporteMensual = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) > DateTime.Now.AddMonths(-1)).Count();
            ViewBag.ReporteTotal = listaRecepciones.ToList().Count();
            return View(listaRecepciones);
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
            ViewBag.ReporteSemanal = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) > DateTime.Now.AddDays(-7)).Count();
            ViewBag.ReporteMensual = listaRecepciones.Where(l => Convert.ToDateTime(l.FechaIngreso) > DateTime.Now.AddMonths(-1)).Count();
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

        //MUCHO CUIDADO
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
