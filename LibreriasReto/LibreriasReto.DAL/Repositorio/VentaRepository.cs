using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<bool> RealizarVenta(Venta venta)
        {
            bool respuesta = false;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var libroEncontrado = _dbContext.Set<Libro>().FirstOrDefault(u => u.IdLibro == venta.IdlibroNavigation.IdLibro);
                    if (libroEncontrado == null) throw new Exception();
                    if (libroEncontrado.Stock < venta.IdlibroNavigation.Stock) throw new Exception();
                    libroEncontrado.Stock -= venta.IdlibroNavigation.Stock;
                    await _dbContext.Set<Venta>().AddAsync(venta);
                    await _dbContext.SaveChangesAsync();
                    respuesta = true;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
            }
            return respuesta;
        }
    }
}
