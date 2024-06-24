using System.Data;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.DAL.Service
{
    public class ReporteExcel
    {

        //public ResultContext GenerarExcel()
        //{
        //    using (XLWorkbook libro = new XLWorkbook())
        //    {
        //        DataTable tabla_cliente = new DataTable();
        //        tabla_cliente.TableName = "Clientes";
        //        var hoja = libro.Worksheets.Add(tabla_cliente);
        //        hoja.ColumnsUsed().AdjustToContents();
        //
        //        using (MemoryStream memoria = new MemoryStream())
        //        {
        //            libro.SaveAs(memoria);
        //            var nombreExcel = string.Concat("Reporte ", DateTime.Now.ToString(), ".xlsx");
        //            return File(memoria.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreExcel);
        //        }
        //    }
        //}

        //public IAsyncResult GenerarExcel(string nombreArchivo, IEnumerable<ClienteDTO> valores)
        //{
        //    DataTable dt = new DataTable("personas");
        //    dt.Columns.AddRange(new DataColumn[]
        //    {
        //        new DataColumn("Id"),
        //        new DataColumn("Dni"),
        //        new DataColumn("Nombre"),
        //        new DataColumn("Apellido"),
        //        new DataColumn("Estado")
        //    });
        //    foreach (var item in valores)
        //    {
        //        dt.Rows.Add(item.IdCliente, item.Dni, item.Nombre, item.Apellido, item.EsActivo);
        //    }
        //    using (XLWorkbook wb = new XLWorkbook())
        //    {
        //        wb.Worksheets.Add(dt);
        //        using (MemoryStream stream = new MemoryStream())
        //        {
        //            wb.SaveAs(stream);
        //            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", nombreArchivo);
        //        }
        //    }
        //}

    }
}
