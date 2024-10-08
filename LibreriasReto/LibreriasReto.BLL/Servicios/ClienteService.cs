﻿using System;
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
    public class ClienteService : IClienteServices
    {
        private readonly LibreriasRetoContext _dbcontext;
        private readonly IMapper _mapper;

        public ClienteService(LibreriasRetoContext dbcontext, IMapper mapper)
        {
            _dbcontext = dbcontext;
            _mapper = mapper;
        }

        public async Task<bool> Actualizar(ClienteDTO cliente)
        {
            bool resultado = false;
            try
            {
                if(cliente != null) 
                {
                    var ClienteEncontrado = await _dbcontext.Set<Cliente>().AsNoTracking().FirstOrDefaultAsync(c => c.IdCliente == cliente.IdCliente);
                    if (ClienteEncontrado == null) throw new TaskCanceledException("Cliente no encontrado");
                    var clienteMapeado = _mapper.Map<Cliente>(cliente);
                    clienteMapeado.EsActivo = ClienteEncontrado.EsActivo;
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

        public async Task<ClienteDTO> Buscar(int id)
        {
            ClienteDTO cliente;
            try
            {
                var clienteEncontrado = await _dbcontext.Set<Cliente>().FirstOrDefaultAsync(u => u.IdCliente == id);
                if(clienteEncontrado == null) throw new TaskCanceledException("Cliente no existe");
                cliente = _mapper.Map<ClienteDTO>(clienteEncontrado);
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
                var clienteEncontrado = await _dbcontext.Set<Cliente>().FirstOrDefaultAsync(u => u.IdCliente == id);
                if (clienteEncontrado == null) throw new TaskCanceledException("Cliente no encontrado");
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

        public async Task<List<ClienteDTO>> Listar()
        {
            List<ClienteDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Cliente>().Where(estado => estado.EsActivo == true).OrderBy(x => x.Nombre).ToListAsync();
                listaClisentes = _mapper.Map<List<ClienteDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<List<ClienteDTO>> ListarDesactivados()
        {
            List<ClienteDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Cliente>().Where(estado => estado.EsActivo == false).OrderBy(x => x.Nombre).ToListAsync();
                listaClisentes = _mapper.Map<List<ClienteDTO>>(clientes);
            }
            catch
            {
                throw;
            }
            return listaClisentes;
        }

        public async Task<List<ClienteDTO>> ListarTodos()
        {
            List<ClienteDTO> listaClisentes;
            try
            {
                var clientes = await _dbcontext.Set<Cliente>().OrderBy(x => x.Nombre).ToListAsync();
                listaClisentes = _mapper.Map<List<ClienteDTO>>(clientes);
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
                Cliente? cliente = await _dbcontext.Set<Cliente>().FindAsync(id);
                if (cliente == null) throw new TaskCanceledException("Cliente no encontrado");                                 
                cliente.EsActivo = true;
                _dbcontext.Set<Cliente>().Update(cliente);
                _dbcontext.SaveChanges();
                resultado = true;
                
            }
            catch
            {
                throw;
            }
            return resultado;
        }

        public async Task<bool> Registrar(ClienteDTO cliente)
        {
            bool resultado = false;
            try
            {
                if (cliente == null) throw new TaskCanceledException("Cliente invalido");
                var usuarioMappeado = _mapper.Map<Cliente>(cliente);
                await _dbcontext.Set<Cliente>().AddAsync(usuarioMappeado);
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
