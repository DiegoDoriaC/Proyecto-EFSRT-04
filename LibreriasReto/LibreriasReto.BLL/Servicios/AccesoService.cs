using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Service;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.BLL.Servicios
{
    public class AccesoService : IAccesoService
    {
        private readonly LibreriasRetoContext _context;
        private readonly IMapper _mapper;

        public AccesoService(LibreriasRetoContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<AccesoDTO> Logueo(int clave, string password)
        {
            AccesoDTO? accesoMappeado;
            try
            {
                string dni = clave.ToString();
                string passwordEncriptado = Encriptacion.GetSHA256(password);
                var listaAcceso = await _context.Set<Acceso>().Include(a => a.IdEmpleadoNavigation).ThenInclude(c => c.IdAreaNavigation).FirstOrDefaultAsync(u => u.dniEmpleado == dni && u.Clave == passwordEncriptado);
                accesoMappeado = _mapper.Map<AccesoDTO>(listaAcceso);
            }
            catch
            {
                throw;
            }
            return accesoMappeado;
        }
    }
}
