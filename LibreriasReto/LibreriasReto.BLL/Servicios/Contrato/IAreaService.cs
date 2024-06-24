using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IAreaService
    {
        Task<List<AreaDTO>> Listar();
        Task<bool> Registrar(AreaDTO area);
        Task<bool> Actualizar(AreaDTO area);
    }
}
