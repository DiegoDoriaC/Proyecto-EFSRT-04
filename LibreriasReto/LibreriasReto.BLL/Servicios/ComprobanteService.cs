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
                    listaComprobante = _mapper.Map<List<ComprobanteDTO>>(await _context.Set<Comprobante>().Where(filtro).ToListAsync());
                }
            }
            catch
            {
                throw;            
            }

            return listaComprobante = _mapper.Map<List<ComprobanteDTO>>(await _context.Comprobantes.ToListAsync());

        }

        public async Task<bool> Registrar(ComprobanteDTO comprobante)
        {
            bool respuesta;
            try
            {
                Comprobante comprobanteMappeado = _mapper.Map<Comprobante>(comprobante);
                await _ventaRepository.RealizarVenta(comprobanteMappeado);
                respuesta = true;
            }
            catch
            {
                throw;
            }
            return respuesta;
        }
    }
}
