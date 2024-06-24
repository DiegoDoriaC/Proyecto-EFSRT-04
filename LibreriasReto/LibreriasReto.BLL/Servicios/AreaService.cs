using System;
using System.Collections.Generic;
using System.Linq;
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
    public class AreaService : IAreaService
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IMapper _mapper;

        public AreaService(LibreriasRetoContext context, IMapper mapper)
        {
            _dbcontext = context;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(AreaDTO area)
        {
            bool respuesta = false;

            try
            {
                if (area != null)
                {
                    area.Sueldo = Math.Round(area.Sueldo, 2);
                    _dbcontext.Set<Area>().Update(_mapper.Map<Area>(area));
                    await _dbcontext.SaveChangesAsync();
                    respuesta = true;
                }
            }
            catch
            {
                throw;
            }
            return respuesta;
        }

        public async Task<List<AreaDTO>> Listar()
        {
            List<AreaDTO> listaAreas;
            try
            {
                var areas = await _dbcontext.Set<Area>().ToListAsync();
                if (areas.Count == 0) throw new TaskCanceledException("Ninguna area encontrada");
                listaAreas = _mapper.Map<List<AreaDTO>>(areas);
            }
            catch
            {
                throw;
            }
            return listaAreas;
        }

        public async Task<bool> Registrar(AreaDTO area)
        {
            bool respuesta = false;
            try
            {
                if (area != null)
                {
                    area.Sueldo = Math.Round(area.Sueldo, 2);
                    var areaMappeada = _mapper.Map<Area>(area);
                    await _dbcontext.Set<Area>().AddAsync(areaMappeada);
                    await _dbcontext.SaveChangesAsync();
                    respuesta = true;
                }
            }
            catch
            {
                throw;
            }
            return respuesta;
        }
    }
}
