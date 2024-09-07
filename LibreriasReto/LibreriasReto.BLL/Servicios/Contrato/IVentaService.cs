using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IVentaService
    {
        Task<List<VentaDTO>> Listar(int id);
    }
}
