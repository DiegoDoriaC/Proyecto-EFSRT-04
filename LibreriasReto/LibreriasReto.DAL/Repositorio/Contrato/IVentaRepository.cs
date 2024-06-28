using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.Models;

namespace LibreriasReto.DAL.Repositorio.Contrato
{
    public interface IVentaRepository
    {
        Task<bool> RealizarVenta(Comprobante comprobante);
    }
}
