using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IGeneroService
    {
        Task<List<GeneroDTO>> Listar();
        Task<bool> Registrar(GeneroDTO genero);
        Task<bool> Actualizar(GeneroDTO genero);

    }
}
