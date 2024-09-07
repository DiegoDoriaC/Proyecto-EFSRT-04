using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;

namespace LibreriasReto.BLL.Servicios.Contrato
{
    public interface IMetodoPagoService
    {
        Task<List<MetodoPagoDTO>> listar(Expression<Func<MetodoPago, bool>> filtro = null);
    }
}
