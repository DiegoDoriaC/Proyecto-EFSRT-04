using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.DAL.Repositorio.Contrato
{
    public interface IEmpleadoRepository
    {
        Task<bool> RegistrarEmpleado(Empleado empleado);
    }
}
