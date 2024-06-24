using ClosedXML.Excel;
using System.Data;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriasReto.Controllers
{
    public class LibroController : Controller
    {
        private readonly ILibroService _servicio;
        private readonly IGeneroService _generoServicio;

        public LibroController(ILibroService servicio, IGeneroService generoService)
        {
            _servicio = servicio;
            _generoServicio = generoService;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var libros = await _servicio.Listar();
            return View(libros);
        }

        [HttpGet]
        public async Task<IActionResult> ListaDesactivados()
        {
            var libros = await _servicio.ListarDesactivados();
            return View(libros);
        }

        [HttpGet]
        public async Task<IActionResult> Active(int id)
        {
            var libros = await _servicio.Activar(id);
            return RedirectToAction(nameof(ListaDesactivados));
        }

        [HttpPost]
        public async Task<IActionResult> Filter(string busqueda = "")
        {
            var libros = await _servicio.Filtrar(busqueda);
            return View(libros);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            return View(await _servicio.Buscar(id));
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.GENEROS = new SelectList(await _generoServicio.Listar(), "IdGenero", "Nombre");
            return View(new LibroDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(LibroDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return RedirectToAction("Lista");
            ViewBag.mensaje = "No se pudo registrar el libro";
            return RedirectToAction("Create");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var libroEncontrado = await _servicio.Buscar(id);
            ViewBag.GENEROS = new SelectList(await _generoServicio.Listar(), "IdGenero", "Nombre", libroEncontrado.IdGenero);
            return View(libroEncontrado);
        }

        [HttpPost]
        public async Task<IActionResult> Update(LibroDTO cliente)
        {
            await _servicio.Actualizar(cliente);
            return RedirectToAction("lista");
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

        [HttpGet]
        public async Task<FileResult> ExportarExcel()
        {
            var listado = await _servicio.ListarTodo();
            var nombreExcel = string.Concat("Reporte Libros : ", DateTime.Now.ToString("dd/MM/yyyy"), ".xlsx");
            return GenerarExcel(nombreExcel, listado);
        }

        public FileResult GenerarExcel(string nombreArchivo, IEnumerable<LibroDTO> valores)
        {
            DataTable dt = new DataTable("personas");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Genero"),
                new DataColumn("Libro"),
                new DataColumn("Autor"),
                new DataColumn("Editorial"),
                new DataColumn("Precio"),
                new DataColumn("Publicacion"),
                new DataColumn("Stock"),
                new DataColumn("Estado")
            });
            foreach (var item in valores)
            {
                dt.Rows.Add(item.IdLibro, item.nombreGenero, item.Nombre, item.Autor, item.Editorial, item.Precio, item.AnioPublicacion, item.Stock, item.EsActivo == true ? "Activo" : "Inactivo");
            }
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
                }
            }
        }

    }
}
