using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

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

        public async Task<ComprobanteDTO> Buscar(int id)
        {
            ComprobanteDTO comprobante = null;
            try
            {
                comprobante = _mapper.Map<ComprobanteDTO>(await _context.Comprobantes.Include(c => c.IdClienteNavigation).Include(c => c.IdEmpleadoNavigation).Include(c => c.IdMetodoPagoNavigation).Where(c => c.IdComprobante == id).FirstOrDefaultAsync());
            }
            catch
            {
                throw;
            }
            return comprobante;
        }

        public async Task<List<ComprobanteDTO>> Listar(Expression<Func<Comprobante, bool>> filtro = null)
        {
            List<ComprobanteDTO> listaComprobante;
            try
            {
                if (filtro != null)
                {
                    List<Comprobante> listaComprobanteSinMappear = await _context.Set<Comprobante>().Where(filtro).Include(u => u.IdMetodoPagoNavigation).Include(u => u.IdClienteNavigation).Include(u => u.IdEmpleadoNavigation).Include(u => u.Venta).ThenInclude(v => v.IdlibroNavigation).ToListAsync();
                    listaComprobante = _mapper.Map<List<ComprobanteDTO>>(listaComprobanteSinMappear);
                    return listaComprobante;
                }
            }
            catch
            {
                throw;
            }

            return listaComprobante = _mapper.Map<List<ComprobanteDTO>>(await _context.Set<Comprobante>().Include(u => u.IdMetodoPagoNavigation).Include(u => u.IdClienteNavigation).Include(u => u.IdEmpleadoNavigation).Include(u => u.Venta).ThenInclude(v => v.IdlibroNavigation).OrderByDescending(x => x.IdComprobante).ToListAsync());

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
