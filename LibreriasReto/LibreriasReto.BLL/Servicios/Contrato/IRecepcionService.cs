using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IRecepcionService
    {
        Task<List<RecepcionDTO>> Listar();
        Task<RecepcionDTO> Buscar(int id);
        Task<bool> Registrar(RecepcionDTO cilente);
    }
}
