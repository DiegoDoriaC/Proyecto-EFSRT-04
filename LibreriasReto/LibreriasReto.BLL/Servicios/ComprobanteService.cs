using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.BLL.Servicios
{
    public class ComprobanteService : IComprobanteServices
    {

        private readonly LibreriasRetoContext _context;
        private readonly IVentaRepository _ventaRepository;
        private readonly IMapper _mapper;

        public ComprobanteService(LibreriasRetoContext context, IVentaRepository ventaRepository, IMapper mapper)
        {
            _context = context;
            _ventaRepository = ventaRepository;
            _mapper = mapper;
        }

        public async Task<List<ComprobanteDTO>> Listar(Expression<Func<Comprobante, bool>> filtro = null)
        {
            List<ComprobanteDTO> listaComprobante;
            try
            {
                if(filtro != null)
                {
                    List<Comprobante>listaComprobanteSinMappear = await _context.Set<Comprobante>().Where(filtro).Include(u => u.IdMetodoPagoNavigation).Include(u => u.IdClienteNavigation).Include(u => u.IdEmpleadoNavigation).Include(u => u.Venta).ThenInclude(v => v.IdlibroNavigation).ToListAsync();
                    listaComprobante = _mapper.Map<List<ComprobanteDTO>>(listaComprobanteSinMappear);
                    return listaComprobante;
                }
            }
            catch
            {
                throw;
            }

            return listaComprobante = _mapper.Map<List<ComprobanteDTO>>(await _context.Set<Comprobante>().Include(u => u.IdMetodoPagoNavigation).Include(u => u.IdClienteNavigation).Include(u => u.IdEmpleadoNavigation).Include(u => u.Venta).ThenInclude(v => v.IdlibroNavigation).ToListAsync());

        }

        public async Task<bool> Registrar(ComprobanteDTO comprobante)
        {
            bool respuesta;
            try
            {
                Comprobante comprobanteMappeado = _mapper.Map<Comprobante>(comprobante);
                respuesta = await _ventaRepository.RealizarVenta(comprobanteMappeado);                
            }
            catch
            {
                throw;
            }
            return respuesta;
        }
    }
}
