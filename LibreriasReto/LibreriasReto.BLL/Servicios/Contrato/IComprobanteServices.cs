using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IComprobanteServices
    {
        Task<List<ComprobanteDTO>> Listar(Expression<Func<Comprobante, bool>> filtro = null);
        Task<ComprobanteDTO> Buscar(int id);
        Task<bool> Registrar(ComprobanteDTO comprobante);
    }
}
