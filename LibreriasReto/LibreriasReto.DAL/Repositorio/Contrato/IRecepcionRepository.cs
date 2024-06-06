using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.DAL.Repositorio.Contrato
{
    public interface IRecepcionRepository
    {
        Task<bool> Registro(RecepcionDTO recepcion);
    }
}
