using LibreriasReto.BLL.Servicios;
using LibreriasReto.BLL.Servicios.Contrato;
using LibreriasReto.DAL.DBContext;
using LibreriasReto.DAL.Repositorio;
using LibreriasReto.DAL.Repositorio.Contrato;
using LibreriasReto.Utilidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LibreriasReto.IOC
{
    public static class Dependencias
    {

        public static void InyectarDependencias(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<LibreriasRetoContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("cadenaSQL"))
            );

            //Crud Repositorio
            services.AddScoped<IRecepcionRepository, RecepcionRepository>();
            services.AddScoped<IVentaRepository, VentaRepository>();
            services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();


            //Crud Servicios
            services.AddScoped<IAccesoService, AccesoService>();
            services.AddScoped<IClienteServices, ClienteService>();
            services.AddScoped<IEmpleadoService, EmpleadoService>();
            services.AddScoped<ILibroService, LibroService>();
            services.AddScoped<IRecepcionService, RecepcionService>();
            services.AddScoped<IGeneroService, GeneroService>();
            services.AddScoped<IAreaService, AreaService>();
            services.AddScoped<IComprobanteServices, ComprobanteService>();
            services.AddScoped<IMetodoPagoService, MetodoPagoService>();
            services.AddScoped<IVentaService, VentaService>();
            services.AddAutoMapper(typeof(AutoMapperProfile));

        }

    }
}
