using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Azure.Core.Pipeline;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.BLL.Servicios
{
    public class RecepcionService : IRecepcionService
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IRecepcionRepository _repository;
        private readonly IMapper _mapper;

        public RecepcionService(LibreriasRetoContext dbcontext, IRecepcionRepository repository, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<RecepcionDTO> Buscar(int id)
        {
            RecepcionDTO cliente;
            try
            {
                var clienteEncontrado = await _dbcontext.Set<Recepcion>().Include(recepcion => recepcion.IdLibroNavigation).FirstOrDefaultAsync(u => u.IdRecepcion == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Recepcion no existe");
                cliente = _mapper.Map<RecepcionDTO>(clienteEncontrado);
            }
            catch
            {
                throw;
            }
            return cliente;
        }

        public async Task<List<RecepcionDTO>> Listar(string nombre = "", string fechaInicio = "", string fechaFin = "")
        {
            List<RecepcionDTO> listaClientes;
            try
            {
                var clientesEncontrados = await _dbcontext.Set<Recepcion>().Include(recepcion => recepcion.IdLibroNavigation).ToListAsync();
                var filtroClientes = clientesEncontrados.ToList();
                if(nombre.Trim() != "") filtroClientes = clientesEncontrados.Where(recepcion => recepcion.IdLibroNavigation.Nombre.ToLower().Contains(nombre.ToLower())).ToList();
                if (fechaInicio.Trim() != "" && fechaFin.Trim() != "") 
                {
                    DateTime fecha_Inicio = Convert.ToDateTime(fechaInicio, new CultureInfo("es-PE"));
                    DateTime fecha_Fin = Convert.ToDateTime(fechaFin, new CultureInfo("es-PE"));
                    filtroClientes = filtroClientes.Where(recepcion => recepcion.FechaIngreso > fecha_Inicio && recepcion.FechaIngreso < fecha_Fin).ToList();
                }
                listaClientes = _mapper.Map<List<RecepcionDTO>>(filtroClientes);
            }
            catch
            {
                throw;
            }
            return listaClientes;
        }

        public async Task<bool> Registrar(RecepcionDTO cliente)
        {
            bool resultado = false;
            try
            {
                var clienteRegistrado = await _repository.Registro(cliente);
                if(clienteRegistrado == true) resultado = true;
            }
            catch
            {
                throw;
            }
            return resultado;
        }
    }
}
