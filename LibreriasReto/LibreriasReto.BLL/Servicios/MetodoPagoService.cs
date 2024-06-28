using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.BLL.Servicios
{
    public class MetodoPagoService : IMetodoPagoService
    {
        private readonly LibreriasRetoContext _context;
        private readonly IMapper _mapper;

        public MetodoPagoService(LibreriasRetoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<MetodoPagoDTO>> listar(Expression<Func<MetodoPago, bool>> filtro = null)
        {
            List<MetodoPagoDTO> listado = _mapper.Map<List<MetodoPagoDTO>>( await _context.MetodoPagos.ToListAsync());
            try
            {
                if (filtro != null)
                {
                    listado = _mapper.Map<List<MetodoPagoDTO>>(await _context.MetodoPagos.Where(filtro).ToListAsync());
                }
            }
            catch
            {
                throw;
            }
            return listado;
        }
    }
}
