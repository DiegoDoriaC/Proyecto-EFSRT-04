using System;
using System.Collections.Generic;
using System.Linq;
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
                var clienteEncontrado = await _dbcontext.Set<Recepcion>().FirstOrDefaultAsync(u => u.IdRecepcion == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Recepcion no existe");
                cliente = _mapper.Map<RecepcionDTO>(clienteEncontrado);
            }
            catch
            {
                throw;
            }
            return cliente;
        }

        public async Task<List<RecepcionDTO>> Listar()
        {
            List<RecepcionDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Recepcion>().ToListAsync();
                if (clientes.Count == 0) throw new TaskCanceledException("Ningun registro encontrado");
                listaClisentes = _mapper.Map<List<RecepcionDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<bool> Registrar(RecepcionDTO cilente)
        {
            bool resultado = false;
            try
            {
                if (cilente == null) throw new TaskCanceledException("Recepcion invalida");
                await _repository.Registro(cilente);
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
