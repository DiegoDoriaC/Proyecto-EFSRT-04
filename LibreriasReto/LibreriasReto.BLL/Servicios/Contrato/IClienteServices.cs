using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IClienteServices
    {
        Task<List<ClienteDTO>> Listar();
        Task<ClienteDTO> Buscar(int id);
        Task<bool> Registrar(ClienteDTO cilente);
        Task<bool> Actualizar(ClienteDTO cliente);
        Task<bool> Eliminar(int id);
    }
}
