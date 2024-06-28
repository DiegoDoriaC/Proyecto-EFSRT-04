using ClosedXML.Excel;
using System.Data;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

using Microsoft.AspNetCore.Authorization;

namespace LibreriasReto.Controllers
{
    [Authorize]
    public class EmpleadoController : Controller
    {
        private readonly IEmpleadoService _servicio;
        private readonly IAreaService _areaService;

        public EmpleadoController(IEmpleadoService servicio, IAreaService areaService)
        {
            _servicio = servicio;
            _areaService = areaService;
        }

        [HttpGet]
        public async Task<IActionResult> Lista()
        {
            var listado = await _servicio.Listar();
            return View(listado);
        }

        [HttpGet]
        public async Task<IActionResult> ListaDesactivados()
        {

            return View(await _servicio.ListarDesactivados());
        }

        [HttpGet]
        public async Task<IActionResult> Active(int id)
        {
            await _servicio.Activar(id);
            return RedirectToAction(nameof(ListaDesactivados));
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.GENEROS = new SelectList(await _areaService.Listar(), "IdArea", "Cargo");
            return View(new EmpleadoDTO());
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmpleadoDTO cliente)
        {
            var respuesta = await _servicio.Registrar(cliente);
            if (respuesta) return RedirectToAction("Lista");
            ViewBag.mensaje = "No se pudo registrar el empleado";
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            EmpleadoDTO empleado = await _servicio.Buscar(id);
            ViewBag.GENEROS = new SelectList(await _areaService.Listar(), "IdArea", "Cargo", empleado.IdArea);
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

        [HttpGet]
        public async Task<FileResult> ExportarExcel()
        {
            var listado = await _servicio.ListarTodo();
            var nombreExcel = string.Concat("Reporte Empleados : ", DateTime.Now.ToString("dd/MM/yyyy"), ".xlsx");
            return GenerarExcel(nombreExcel, listado);
        }

        public FileResult GenerarExcel(string nombreArchivo, IEnumerable<EmpleadoDTO> valores)
        {
            DataTable dt = new DataTable("personas");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("ID"),
                new DataColumn("Area"),
                new DataColumn("DNI"),
                new DataColumn("Nombre"),
                new DataColumn("Apellido"),
                new DataColumn("Telefono"),
                new DataColumn("Fecha Ingreso"),
                new DataColumn("Email"),
                new DataColumn("Direccion"),
                new DataColumn("Estado")
            });
            foreach (var item in valores)
            {
                dt.Rows.Add(item.IdEmpleado, item.nombreArea, item.Dni, item.Nombre, item.Apellido, item.Telefono, item.FechaIngreso, item.Email, item.Direccion, item.EsActivo == true ? "Activo" : "Inactivo");
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
