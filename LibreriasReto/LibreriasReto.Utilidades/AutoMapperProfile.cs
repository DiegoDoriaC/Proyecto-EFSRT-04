using System.Globalization;
using AutoMapper;
using LibreriasReto.DTO;
using LibreriasReto.Models;

namespace LibreriasReto.Utilidades
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {

            #region Acceso
            CreateMap<Acceso, AccesoDTO>()
                .ForMember(destino => destino.EmpleadoNombre, options => options.MapFrom(origen => origen.IdEmpleadoNavigation.Nombre));
            #endregion Acceso

            #region AccesoDTO
            CreateMap<AccesoDTO, Acceso>()
                .ForMember(destino => destino.IdEmpleadoNavigation, options => options.Ignore());
            #endregion AccesoDTO

            #region Area
            CreateMap<Area, AreaDTO>().ReverseMap();
            #endregion Area

            #region Cliente
            CreateMap<Cliente, ClienteDTO>().ReverseMap();
            #endregion Cliente

            #region Empleado
            CreateMap<Empleado, EmpleadoDTO>()
                .ForMember(d => d.nombreArea, op => op.MapFrom(or => or.IdAreaNavigation.Cargo))
                .ForMember(d => d.FechaIngreso, op => op.MapFrom(or => or.FechaIngreso.Value.ToString("dd/MM/yyyy", new CultureInfo("es-PE"))));
            #endregion Empleado

            #region EmpleadoDTO
            CreateMap<EmpleadoDTO, Empleado>()
                .ForMember(d => d.IdAreaNavigation, op => op.Ignore())
                .ForMember(d => d.FechaIngreso, op => op.MapFrom(or => Convert.ToDateTime(or.FechaIngreso, new CultureInfo("es-PE"))));
            #endregion EmpleadoDTO

            #region Genero
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            #endregion Genero

            #region Libro
            CreateMap<Libro, LibroDTO>()
                .ForMember(d => d.nombreGenero, op => op.MapFrom(or => or.IdGeneroNavigation.Nombre))
                .ForMember(d => d.Precio, op => op.MapFrom(or => Convert.ToString(or.Precio)))
                .ForMember(d => d.AnioPublicacion, op => op.MapFrom(or => Convert.ToString(or.AnioPublicacion)));
            #endregion Libro

            #region LibroDTO
            CreateMap<LibroDTO, Libro>()
                .ForMember(d => d.IdGeneroNavigation, op => op.Ignore())
                .ForMember(d => d.Precio, op => op.MapFrom(or => Convert.ToInt32(or.Precio)))
                .ForMember(d => d.AnioPublicacion, op => op.MapFrom(or => Convert.ToInt32(or.AnioPublicacion)));
            #endregion LibroDTO

            #region MetodoPago
            CreateMap<MetodoPago, MetodoPagoDTO>().ReverseMap();
            #endregion MetodoPago

            #region Recepcion
            CreateMap<Recepcion, RecepcionDTO>()
                .ForMember(d => d.nombreLibro, op => op.MapFrom(or => or.IdLibroNavigation.Nombre))
                .ForMember(d => d.FechaIngreso, op => op.MapFrom(or => or.FechaIngreso.Value.ToString("dd/MM/yyyy", new CultureInfo("es-PE"))));
            #endregion Recepcion

            #region RecepcionDTO
            CreateMap<RecepcionDTO, Recepcion>()
                .ForMember(d => d.IdLibroNavigation, op => op.Ignore())
                .ForMember(d => d.FechaIngreso, op => op.MapFrom(or => Convert.ToDateTime(or.FechaIngreso, new CultureInfo("es-PE"))));
            #endregion RecepcionDTO

            #region Venta
            CreateMap<Venta, VentaDTO>()
                .ForMember(d => d.nombreLibro, op => op.MapFrom(or => or.IdlibroNavigation.Nombre));
            #endregion Venta

            #region VentaDTO
            CreateMap<VentaDTO, Venta>()
                .ForMember(d => d.IdlibroNavigation, op => op.Ignore());
            #endregion VentaDTO

            #region Comprobante
            CreateMap<Comprobante, ComprobanteDTO>()
                .ForMember(d => d.nombreCliente, op => op.MapFrom(or => or.IdClienteNavigation.Nombre))
                .ForMember(d => d.nombreEmpleado, op => op.MapFrom(or => or.IdEmpleadoNavigation.Nombre))
                .ForMember(d => d.nombreMetodoPago, op => op.MapFrom(or => or.IdMetodoPagoNavigation.Nombre))
                .ForMember(d => d.FechaVenta, op => op.MapFrom(or => or.FechaVenta.Value.ToString("dd/MM/yyyy", new CultureInfo("es-PE"))));
            #endregion Comprobante

            #region ComprobanteDTO
            CreateMap<ComprobanteDTO, Comprobante>()
                .ForMember(d => d.IdClienteNavigation, op => op.Ignore())
                .ForMember(d => d.IdEmpleadoNavigation, op => op.Ignore())
                .ForMember(d => d.IdMetodoPagoNavigation, op => op.Ignore())
                .ForMember(d => d.FechaVenta, op => op.MapFrom(or => Convert.ToDateTime(or.FechaVenta, new CultureInfo("es-PE"))));
            #endregion ComprobanteDTO

        }
    }
}
