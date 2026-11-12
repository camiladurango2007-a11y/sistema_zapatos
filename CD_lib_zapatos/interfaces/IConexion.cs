using CD_lib_zapatos.entidades;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace CD_lib_zapatos.interfaces;

public interface IConexion
{
    DbSet<Zapato> Zapatos { get; set; }
    DbSet<Cliente> Clientes { get; set; }
    DbSet<Venta> Ventas { get; set; }
    EntityEntry<T> Entry<T>(T entity) where T : class;
    DbSet<T> Set<T>() where T : class;
    int SaveChanges();
}
