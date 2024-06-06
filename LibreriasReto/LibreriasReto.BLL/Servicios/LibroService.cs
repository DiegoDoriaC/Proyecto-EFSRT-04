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
    public class LibroService : ILibroService
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IMapper _mapper;

        public LibroService(LibreriasRetoContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(LibroDTO libro)
        {
            bool resultado = false;
            try
            {
                if (libro != null)
                {
                    var clienteMapeado = _mapper.Map<Libro>(libro);
                    _dbcontext.Update(clienteMapeado);
                    await _dbcontext.SaveChangesAsync();
                    resultado = true;
                }
            }
            catch
            {
                throw;
            }
            return resultado;
        }

        public async Task<LibroDTO> Buscar(int id)
        {
            LibroDTO cliente;
            try
            {
                var clienteEncontrado = await _dbcontext.Set<Libro>().FirstOrDefaultAsync(u => u.IdLibro == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Libro no existe");
                cliente = _mapper.Map<LibroDTO>(clienteEncontrado);
            }
            catch
            {
                throw;
            }
            return cliente;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool resultado = false;
            try
            {
                var clienteEncontrado = await _dbcontext.Set<Libro>().FirstOrDefaultAsync(u => u.IdLibro == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Libro no encontrado");
                clienteEncontrado.EsActivo = false;
                _dbcontext.Update(clienteEncontrado);
                await _dbcontext.SaveChangesAsync();
                resultado = true;
            }
            catch
            {
                throw;
            }
            return resultado;
        }

        public async Task<List<LibroDTO>> Listar()
        {
            List<LibroDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Libro>().Where(activo => activo.EsActivo == true).Include(i => i.IdGeneroNavigation).ToListAsync();
                if (clientes.Count == 0) throw new TaskCanceledException("Ningun registro encontrado");
                listaClisentes = _mapper.Map<List<LibroDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<bool> Registrar(LibroDTO libro)
        {
            bool resultado = false;
            try
            {
                if (libro == null) throw new TaskCanceledException("Libro invalido");
                var usuarioMappeado = _mapper.Map<Libro>(libro);
                await _dbcontext.Set<Libro>().AddAsync(usuarioMappeado);
                await _dbcontext.SaveChangesAsync();
                resultado = true;
            }
            catch
            {
                throw;
            }
            return resultado;
        }
    }
}
