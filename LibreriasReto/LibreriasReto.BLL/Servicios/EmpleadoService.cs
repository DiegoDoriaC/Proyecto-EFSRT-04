using AutoMapper;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.BLL.Servicios
{
    public class EmpleadoService : IEmpleadoService
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IEmpleadoRepository _repository;
        private readonly IMapper _mapper;

        public EmpleadoService(LibreriasRetoContext dbcontext, IEmpleadoRepository repository, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(EmpleadoDTO empleado)
        {
            bool resultado = false;
            try
            {
                if (empleado != null)
                {
                    var clienteMapeado = _mapper.Map<Empleado>(empleado);
                    var clienteEncontrado = await _dbcontext.Set<Empleado>().FindAsync(clienteMapeado.IdEmpleado);
                    await _dbcontext.SaveChangesAsync();
                    if (clienteEncontrado == null) { throw new TaskCanceledException("Empleado no encontrado"); }
                    _dbcontext.Update(clienteEncontrado);
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

        public async Task<EmpleadoDTO> Buscar(int id)
        {
            EmpleadoDTO cliente;
            try
            {
                var clienteEncontrado = await _dbcontext.Set<Empleado>().FirstOrDefaultAsync(u => u.IdEmpleado == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Empleado no existe");
                cliente = _mapper.Map<EmpleadoDTO>(clienteEncontrado);
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
                var clienteEncontrado = await _dbcontext.Set<Empleado>().FirstOrDefaultAsync(u => u.IdEmpleado == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Empleado no encontrado");
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

        public async Task<List<EmpleadoDTO>> Listar()
        {
            List<EmpleadoDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Empleado>().Where(estado => estado.EsActivo == true).Include(c => c.IdAreaNavigation).ToListAsync();
                listaClisentes = _mapper.Map<List<EmpleadoDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<bool> Registrar(EmpleadoDTO empleado)
        {
            bool resultado = false;
            try
            {
                if (empleado == null) throw new TaskCanceledException("Empleado invalido");
                var usuarioMappeado = _mapper.Map<Empleado>(empleado);
                await _repository.RegistrarEmpleado(usuarioMappeado);
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
