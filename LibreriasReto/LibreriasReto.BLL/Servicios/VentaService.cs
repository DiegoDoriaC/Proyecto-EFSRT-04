using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DTO;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriasReto.BLL.Servicios
{
    public class VentaService : IVentaService
    {
        private readonly LibreriasRetoContext _context;
        private readonly IMapper _mapper;

        public VentaService(LibreriasRetoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<VentaDTO>> Listar(int id)
        {
            List<VentaDTO> listadoVenta;
            try
            {
                listadoVenta = _mapper.Map<List<VentaDTO>>(await _context.Venta.Include(v => v.IdlibroNavigation).Where(v => v.IdComprobante == id).ToListAsync());                
            }
            catch
            {
                throw;
            }
            return listadoVenta;
        }
    }
}
