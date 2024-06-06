using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface ILibroService
    {
        Task<List<LibroDTO>> Listar();
        Task<LibroDTO> Buscar(int id);
        Task<bool> Registrar(LibroDTO libro);
        Task<bool> Actualizar(LibroDTO libro);
        Task<bool> Eliminar(int id);
    }
}
