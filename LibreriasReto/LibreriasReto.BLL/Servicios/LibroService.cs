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
                    var libroEncontrado = await _dbcontext.Set<Libro>().AsNoTracking().FirstOrDefaultAsync(l => l.IdLibro == libro.IdLibro);
                    if (libroEncontrado == null) throw new TaskCanceledException("Libro no encontrado");
                    var libroMapeado = _mapper.Map<Libro>(libro);
                    libroMapeado.EsActivo = libroEncontrado.EsActivo;
                    libroMapeado.Stock = libroEncontrado.Stock;
                    _dbcontext.Update(libroMapeado);
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
                var clienteEncontrado = await _dbcontext.Set<Libro>().Include(libro => libro.IdGeneroNavigation).FirstOrDefaultAsync(u => u.IdLibro == id);
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
                listaClisentes = _mapper.Map<List<LibroDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<List<LibroDTO>> ListarTodo()
        {
            List<LibroDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Libro>().Include(libro => libro.IdGeneroNavigation).ToListAsync();
                listaClisentes = _mapper.Map<List<LibroDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<List<LibroDTO>> Filtrar(string nombre)
        {
            List<LibroDTO> listaClisentes;
            List<Libro> librosConFiltro;
            try
            {
                librosConFiltro = nombre == null || nombre.Trim() == "" ? await _dbcontext.Set<Libro>().Where(activo => activo.EsActivo == true).Include(i => i.IdGeneroNavigation).ToListAsync() : await _dbcontext.Set<Libro>().Where(libro => libro.EsActivo == true && libro.Nombre.Contains(nombre)).Include(i => i.IdGeneroNavigation).ToListAsync();
                listaClisentes = _mapper.Map<List<LibroDTO>>(librosConFiltro);
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
                var libroCreado = await _dbcontext.Set<Libro>().AddAsync(usuarioMappeado);
                await _dbcontext.SaveChangesAsync();
                if(libroCreado.Entity.IdLibro != 0)
                resultado = true;
            }
            catch
            {
                throw;
            }
            return resultado;
        }

        public async Task<List<LibroDTO>> ListarDesactivados()
        {
            List<LibroDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Libro>().Where(activo => activo.EsActivo == false).Include(i => i.IdGeneroNavigation).ToListAsync();
                listaClisentes = _mapper.Map<List<LibroDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<bool> Activar(int id)
        {
            bool resultado = false;
            try
            {
                Libro? libro = await _dbcontext.Set<Libro>().FindAsync(id);
                if (libro == null) throw new TaskCanceledException("Libro no encontrado");
                libro.EsActivo = true;
                _dbcontext.Set<Libro>().Update(libro);
                _dbcontext.SaveChanges();
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
