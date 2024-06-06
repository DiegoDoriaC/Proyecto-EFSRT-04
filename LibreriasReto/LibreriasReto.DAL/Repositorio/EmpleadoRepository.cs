using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.DAL.Service;
using LibreriasReto.Models;

namespace LibreriasReto.DAL.Repositorio
{
    public class EmpleadoRepository : IEmpleadoRepository
    {
        private readonly LibreriasRetoContext _dbContext;

        public EmpleadoRepository(LibreriasRetoContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> RegistrarEmpleado(Empleado empleado)
        {
            bool resultado = false;
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    if (empleado == null) throw new TaskCanceledException("Empleado invalido");

                    var empleados = await _dbContext.Set<Empleado>().AddAsync(empleado);
                    await _dbContext.SaveChangesAsync();
                    Acceso acceso = new Acceso()
                    {
                        IdEmpleado = empleados.Entity.IdEmpleado,
                        dniEmpleado = empleado.Dni,
                        Clave = Encriptacion.GetSHA256(empleado.Dni + empleado.Telefono.Substring(empleado.Telefono.Length - 2))
                    };
                    await _dbContext.Set<Acceso>().AddAsync(acceso);
                    await _dbContext.SaveChangesAsync();
                    resultado = true;
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                }
                return resultado;
            }
        }
    }
}
