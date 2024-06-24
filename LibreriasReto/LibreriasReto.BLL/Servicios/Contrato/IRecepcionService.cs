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
        Task<List<RecepcionDTO>> Listar(string nombre="", string fechaInicio="", string fechaFin="");
        Task<RecepcionDTO> Buscar(int id);
        Task<bool> Registrar(RecepcionDTO cilente);
    }
}
