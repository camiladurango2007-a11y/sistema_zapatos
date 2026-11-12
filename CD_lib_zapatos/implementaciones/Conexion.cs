using CD_lib_zapatos.entidades;
using CD_lib_zapatos.interfaces;
using CD_lib_zapatos.nucleo;
using Microsoft.EntityFrameworkCore;

namespace CD_lib_zapatos.implementaciones;

public class Conexion : DbContext, IConexion
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(DatosGenerales.StringConexion());
    }
    public DbSet<Zapato> Zapatos { get; set; } = null!;
    public DbSet<Cliente> Clientes { get; set; } = null!;
    public DbSet<Venta> Ventas { get; set; } = null!;
}
