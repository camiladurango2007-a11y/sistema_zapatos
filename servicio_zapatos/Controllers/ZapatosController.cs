using CD_lib_zapatos.entidades;
using CD_lib_zapatos.interfaces;
using Microsoft.AspNetCore.Mvc;

namespace servicio_zapatos.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class ZapatosController : ControllerBase
{
    private readonly IEntidadAplicacion<Zapato> aplicacion;
    public ZapatosController(IEntidadAplicacion<Zapato> aplicacion) => this.aplicacion = aplicacion;
    [HttpPost] public List<Zapato> Consultar() => aplicacion.Consultar();
    [HttpPost] public Zapato Insertar(Zapato entidad) => aplicacion.Insertar(entidad);
    [HttpPost] public Zapato Actualizar(Zapato entidad) => aplicacion.Actualizar(entidad);
    [HttpDelete("{id:int}")] public Zapato Borrar(int id) => aplicacion.Borrar(id);
}
