using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LibreriasReto.Models;

public partial class LibreriasRetoContext : DbContext
{
    public LibreriasRetoContext()
    {
    }

    public LibreriasRetoContext(DbContextOptions<LibreriasRetoContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Acceso> Accesos { get; set; }

    public virtual DbSet<Area> Areas { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Comprobante> Comprobantes { get; set; }

    public virtual DbSet<Empleado> Empleados { get; set; }

    public virtual DbSet<Genero> Generos { get; set; }

    public virtual DbSet<Libro> Libros { get; set; }

    public virtual DbSet<MetodoPago> MetodoPagos { get; set; }

    public virtual DbSet<Recepcion> Recepcions { get; set; }

    public virtual DbSet<Venta> Venta { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Acceso>(entity =>
        {
            entity.HasKey(e => e.IdAcceso).HasName("PK__acceso__FF9376626BF0958F");

            entity.ToTable("acceso");

            entity.Property(e => e.IdAcceso).HasColumnName("idAcceso");
            entity.Property(e => e.Clave)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("clave");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Accesos)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__acceso__idEmplea__4316F928");
        });

        modelBuilder.Entity<Area>(entity =>
        {
            entity.HasKey(e => e.IdArea).HasName("PK__area__750ECEA464C077F3");

            entity.ToTable("area");

            entity.Property(e => e.IdArea).HasColumnName("idArea");
            entity.Property(e => e.Cargo)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("cargo");
            entity.Property(e => e.Sueldo)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("sueldo");
        });

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.HasKey(e => e.IdCliente).HasName("PK__cliente__885457EEB89800A1");

            entity.ToTable("cliente");

            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Dni)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Comprobante>(entity =>
        {
            entity.HasKey(e => e.IdComprobante).HasName("PK__comproba__BF4D8CF39CD77BBF");

            entity.ToTable("comprobante");

            entity.Property(e => e.IdComprobante).HasColumnName("idComprobante");
            entity.Property(e => e.FechaVenta)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("fechaVenta");
            entity.Property(e => e.IdCliente).HasColumnName("idCliente");
            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.IdMetodoPago).HasColumnName("idMetodoPago");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdClienteNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.IdCliente)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comproban__idCli__4D94879B");

            entity.HasOne(d => d.IdEmpleadoNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.IdEmpleado)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comproban__idEmp__4E88ABD4");

            entity.HasOne(d => d.IdMetodoPagoNavigation).WithMany(p => p.Comprobantes)
                .HasForeignKey(d => d.IdMetodoPago)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__comproban__idMet__4F7CD00D");
        });

        modelBuilder.Entity<Empleado>(entity =>
        {
            entity.HasKey(e => e.IdEmpleado).HasName("PK__empleado__5295297CC8C43EFE");

            entity.ToTable("empleado");

            entity.Property(e => e.IdEmpleado).HasColumnName("idEmpleado");
            entity.Property(e => e.Apellido)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("apellido");
            entity.Property(e => e.Direccion)
                .HasMaxLength(40)
                .IsUnicode(false)
                .HasColumnName("direccion");
            entity.Property(e => e.Dni)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("dni");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.IdArea).HasColumnName("idArea");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(9)
                .IsUnicode(false)
                .HasColumnName("telefono");

            entity.HasOne(d => d.IdAreaNavigation).WithMany(p => p.Empleados)
                .HasForeignKey(d => d.IdArea)
                .HasConstraintName("FK__empleado__idArea__3F466844");
        });

        modelBuilder.Entity<Genero>(entity =>
        {
            entity.HasKey(e => e.IdGenero).HasName("PK__genero__85223DA393300391");

            entity.ToTable("genero");

            entity.Property(e => e.IdGenero).HasColumnName("idGenero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Libro>(entity =>
        {
            entity.HasKey(e => e.IdLibro).HasName("PK__libro__5BC0F026139760BD");

            entity.ToTable("libro");

            entity.Property(e => e.IdLibro).HasColumnName("idLibro");
            entity.Property(e => e.AnioPublicacion).HasColumnName("anioPublicacion");
            entity.Property(e => e.Autor)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("autor");
            entity.Property(e => e.Editorial)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("editorial");
            entity.Property(e => e.EsActivo)
                .HasDefaultValue(true)
                .HasColumnName("esActivo");
            entity.Property(e => e.IdGenero).HasColumnName("idGenero");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nombre");
            entity.Property(e => e.Precio).HasColumnName("precio");
            entity.Property(e => e.Stock)
                .HasDefaultValue(0)
                .HasColumnName("stock");

            entity.HasOne(d => d.IdGeneroNavigation).WithMany(p => p.Libros)
                .HasForeignKey(d => d.IdGenero)
                .HasConstraintName("FK__libro__idGenero__45F365D3");
        });

        modelBuilder.Entity<MetodoPago>(entity =>
        {
            entity.HasKey(e => e.IdMetodoPago).HasName("PK__metodoPa__817BFC32FF2BBAB3");

            entity.ToTable("metodoPago");

            entity.Property(e => e.IdMetodoPago).HasColumnName("idMetodoPago");
            entity.Property(e => e.Nombre)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Recepcion>(entity =>
        {
            entity.HasKey(e => e.IdRecepcion).HasName("PK__recepcio__82211839C648BFE6");

            entity.ToTable("recepcion");

            entity.Property(e => e.IdRecepcion).HasColumnName("idRecepcion");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.FechaIngreso).HasColumnName("fechaIngreso");
            entity.Property(e => e.IdLibro).HasColumnName("idLibro");

            entity.HasOne(d => d.IdLibroNavigation).WithMany(p => p.Recepcions)
                .HasForeignKey(d => d.IdLibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__recepcion__idLib__4AB81AF0");
        });

        modelBuilder.Entity<Venta>(entity =>
        {
            entity.HasKey(e => e.IdVenta).HasName("PK__venta__077D5614759F082B");

            entity.ToTable("venta");

            entity.Property(e => e.IdVenta).HasColumnName("idVenta");
            entity.Property(e => e.Cantidad).HasColumnName("cantidad");
            entity.Property(e => e.IdComprobante).HasColumnName("idComprobante");
            entity.Property(e => e.Idlibro).HasColumnName("idlibro");
            entity.Property(e => e.Precio)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("precio");
            entity.Property(e => e.Total)
                .HasColumnType("decimal(10, 2)")
                .HasColumnName("total");

            entity.HasOne(d => d.IdComprobanteNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.IdComprobante)
                .HasConstraintName("FK__venta__idComprob__534D60F1");

            entity.HasOne(d => d.IdlibroNavigation).WithMany(p => p.Venta)
                .HasForeignKey(d => d.Idlibro)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__venta__idlibro__5441852A");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
