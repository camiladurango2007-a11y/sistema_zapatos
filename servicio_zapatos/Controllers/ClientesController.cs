using CD_lib_zapatos.entidades;
using CD_lib_zapatos.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_zapatos.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ClientesController : ControllerBase
{
    private readonly IEntidadAplicacion<Cliente> aplicacion;
    public ClientesController(IEntidadAplicacion<Cliente> aplicacion) => this.aplicacion = aplicacion;
    [HttpPost] public List<Cliente> Consultar() => aplicacion.Consultar();
    [HttpPost] public Cliente Insertar(Cliente entidad) => aplicacion.Insertar(entidad);
    [HttpPost] public Cliente Actualizar(Cliente entidad) => aplicacion.Actualizar(entidad);
    [HttpDelete("{id:int}")] public Cliente Borrar(int id) => aplicacion.Borrar(id);
}
