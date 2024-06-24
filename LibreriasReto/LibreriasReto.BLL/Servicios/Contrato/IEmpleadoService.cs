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
        Task<List<EmpleadoDTO>> ListarTodo();
        Task<List<EmpleadoDTO>> ListarDesactivados();
        Task<bool> Activar(int id);
        Task<EmpleadoDTO> Buscar(int id);
        Task<bool> Registrar(EmpleadoDTO empleado);
        Task<bool> Actualizar(EmpleadoDTO empleado);
        Task<bool> Eliminar(int id);
    }
}
