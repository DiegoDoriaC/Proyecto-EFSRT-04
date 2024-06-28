using ClosedXML.Excel;
using System.Data;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DTO;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;

namespace LibreriasReto.Controllers
{
    [Authorize]
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

        [HttpGet]
        public async Task<FileResult> ExportarExcel()
        {
            var listado = await _servicio.ListarTodos();
            var nombreExcel = string.Concat("Reporte Clientes : ", DateTime.Now.ToString("dd/MM/yyyy"), ".xlsx");
            return GenerarExcel(nombreExcel, listado);
        }

        public FileResult GenerarExcel(string nombreArchivo, IEnumerable<ClienteDTO> valores)
        {
            DataTable dt = new DataTable("personas");
            dt.Columns.AddRange(new DataColumn[]
            {
                new DataColumn("Id"),
                new DataColumn("Dni"),
                new DataColumn("Nombre"),
                new DataColumn("Apellido"),
                new DataColumn("Estado")
            });
            foreach (var item in valores)
            {
                dt.Rows.Add(item.IdCliente, item.Dni, item.Nombre, item.Apellido, item.EsActivo == true ? "Activo" : "Inactivo");
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
