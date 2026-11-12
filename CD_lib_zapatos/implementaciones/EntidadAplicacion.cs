using CD_lib_zapatos.interfaces;
using Microsoft.EntityFrameworkCore;

namespace CD_lib_zapatos.implementaciones;

public class EntidadAplicacion<T> : IEntidadAplicacion<T> where T : class
{
    private readonly IConexion conexion;
    public EntidadAplicacion(IConexion conexion) => this.conexion = conexion;

    public T Insertar(T entidad)
    {
        ArgumentNullException.ThrowIfNull(entidad);
        conexion.Set<T>().Add(entidad);
        conexion.SaveChanges();
        return entidad;
    }
    public T Actualizar(T entidad)
    {
        ArgumentNullException.ThrowIfNull(entidad);
        conexion.Entry(entidad).State = EntityState.Modified;
        conexion.SaveChanges();
        return entidad;
    }
    public List<T> Consultar() => conexion.Set<T>().AsNoTracking().Take(100).ToList();
    public T Borrar(int id)
    {
        var entidad = conexion.Set<T>().Find(id) ?? throw new KeyNotFoundException("Registro no encontrado.");
        conexion.Set<T>().Remove(entidad);
        conexion.SaveChanges();
        return entidad;
    }
}
