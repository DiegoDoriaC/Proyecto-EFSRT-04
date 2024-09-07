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
    public class GeneroService : IGeneroService
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IMapper _mapper;

        public GeneroService(LibreriasRetoContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }      

        public async Task<bool> Actualizar(GeneroDTO genero)
        {
            bool respuesta = false;

            try
            {
                if (genero != null)
                {
                    _dbcontext.Set<Genero>().Update(_mapper.Map<Genero>(genero));
                    await _dbcontext.SaveChangesAsync();
                    respuesta =  true;
                }
            }
            catch
            {
                throw;
            }
            return respuesta;
        }

        public async Task<List<GeneroDTO>> Listar()
        {
            List<GeneroDTO> listaGeneros;
            try
            {
                var generos = await _dbcontext.Set<Genero>().OrderBy(x => x.Nombre).ToListAsync();
                if (generos.Count == 0) throw new TaskCanceledException("Ningun Genero encontrado");
                listaGeneros = _mapper.Map<List<GeneroDTO>>(generos);
            }
            catch
            {
                throw;
            }
            return listaGeneros;
        }

        public async Task<bool> Registrar(GeneroDTO genero)
        {
            bool respuesta = false;
            
            try
            {
                if(genero != null)
                {
                    var generoGuardado = await _dbcontext.Set<Genero>().AddAsync(_mapper.Map<Genero>(genero));
                    await _dbcontext.SaveChangesAsync();
                    if (generoGuardado.Entity.IdGenero != 0)
                    {
                        respuesta = true;
                    }
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
