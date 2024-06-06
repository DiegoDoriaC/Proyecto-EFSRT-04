using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IComprobanteServices
    {
        Task<List<ComprobanteDTO>> Listar();
        Task<ComprobanteDTO> Buscar(int id);
        Task<bool> Registrar(ComprobanteDTO comprobante);
    }
}
