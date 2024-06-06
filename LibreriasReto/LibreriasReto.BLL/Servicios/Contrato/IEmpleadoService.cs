using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IEmpleadoService
    {
        Task<List<EmpleadoDTO>> Listar();
        Task<EmpleadoDTO> Buscar(int id);
        Task<bool> Registrar(EmpleadoDTO empleado);
        Task<bool> Actualizar(EmpleadoDTO empleado);
        Task<bool> Eliminar(int id);
    }
}
