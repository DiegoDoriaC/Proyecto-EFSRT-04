using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DTO;
using LibreriasReto.Models;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace LibreriasReto.DAL.Repositorio
{
    public class RecepcionRepository : IRecepcionRepository
    {
        private readonly LibreriasRetoContext _dbContext;
        private readonly IMapper _mapper;

        public RecepcionRepository(LibreriasRetoContext repository, IMapper mapper)
        {
            _dbContext = repository;
            _mapper = mapper;
        }

        public async Task<bool> Registro(RecepcionDTO recepcion)
        {
            var recepcionMappeado = _mapper.Map<Recepcion>(recepcion);
            bool estado = false;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var listaLibro = await _dbContext.Set<Libro>().ToListAsync();
                    var libroEncontrado = listaLibro.Where(u => u.IdLibro == recepcion.IdLibro).FirstOrDefault();
                    if (libroEncontrado == null) throw new Exception();
                    libroEncontrado.Stock += recepcion.Cantidad;
                    var libroModificado = _dbContext.Set<Libro>().Update(libroEncontrado);
                    var registro = await _dbContext.Set<Recepcion>().AddAsync(recepcionMappeado);
                    await _dbContext.SaveChangesAsync();
                    estado = true;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                return estado;
            }
        }
    }
}
