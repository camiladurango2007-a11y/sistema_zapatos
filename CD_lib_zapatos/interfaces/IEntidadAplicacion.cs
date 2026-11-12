namespace CD_lib_zapatos.interfaces;

public interface IEntidadAplicacion<T> where T : class
{
    T Insertar(T entidad);
    T Actualizar(T entidad);
    List<T> Consultar();
    T Borrar(int id);
}
