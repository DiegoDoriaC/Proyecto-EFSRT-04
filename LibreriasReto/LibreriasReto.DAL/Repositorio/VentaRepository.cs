using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DocumentFormat.OpenXml.Drawing;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.Models;

namespace LibreriasReto.DAL.Repositorio
{
    public class VentaRepository : IVentaRepository
    {

        private readonly LibreriasRetoContext _dbContext;

        public VentaRepository(LibreriasRetoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RealizarVenta(Comprobante comprobante)
        {
            bool respuesta;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    decimal? importeTotal = 0;
                    foreach (Venta item in comprobante.Venta)
                    {
                        var libroEncontrado = _dbContext.Set<Libro>().FirstOrDefault(u => u.IdLibro == item.Idlibro)!;
                        if(libroEncontrado.Stock < item.Cantidad || item.Cantidad <= 0) return respuesta = false; 
                        libroEncontrado.Stock -= item.Cantidad;
                        _dbContext.Set<Libro>().Update(libroEncontrado);     
                        item.Precio = libroEncontrado.Precio;
                        item.Total = item.Precio * item.Cantidad;
                        importeTotal += item.Total;
                    }
                    await _dbContext.SaveChangesAsync();
                    comprobante.Total = importeTotal;
                    comprobante.FechaVenta = null;
                    await _dbContext.Set<Comprobante>().AddAsync(comprobante);
                    await _dbContext.SaveChangesAsync();
                    respuesta = true;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
            return respuesta;
        }
    }
}
