using System;
using System.Collections.Generic;

namespace LibreriasReto.Models;

public partial class Comprobante
{
    public int IdComprobante { get; set; }

    public int IdCliente { get; set; }

    public int IdEmpleado { get; set; }

    public int IdMetodoPago { get; set; }

    public decimal? Total { get; set; }

    public DateOnly? FechaVenta { get; set; }

    public virtual Cliente IdClienteNavigation { get; set; } = null!;

    public virtual Empleado IdEmpleadoNavigation { get; set; } = null!;

    public virtual MetodoPago IdMetodoPagoNavigation { get; set; } = null!;

    public virtual ICollection<Venta> Venta { get; set; } = new List<Venta>();
}
