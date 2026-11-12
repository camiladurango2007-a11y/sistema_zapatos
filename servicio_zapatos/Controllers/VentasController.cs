using CD_lib_zapatos.entidades;
using CD_lib_zapatos.interfaces;
using CD_lib_zapatos.nucleo;
using Microsoft.AspNetCore.Mvc;

namespace servicio_zapatos.Controllers;

// Resumen de la venta. No es una entidad de base de datos y, por tanto,
// no agrega atributos a la entidad Venta.
public record ResultadoVenta(
    Venta Venta,
    decimal PrecioUnitario,
    decimal Subtotal,
    decimal PorcentajeDescuento,
    decimal ValorDescuento,
    decimal Total);

[ApiController]
[Route("[controller]/[action]")]
public class VentasController : ControllerBase
{
    private readonly IEntidadAplicacion<Venta> aplicacion;
    private readonly IConexion conexion;

    public VentasController(IEntidadAplicacion<Venta> aplicacion, IConexion conexion)
    {
        this.aplicacion = aplicacion;
        this.conexion = conexion;
    }

    [HttpPost]
    public List<Venta> Consultar() => aplicacion.Consultar();

    // Registra la venta y calcula automáticamente el descuento según el caso.
    [HttpPost]
    public ActionResult<ResultadoVenta> Insertar(Venta entidad)
    {
        if (entidad.Cantidad < 1)
            return BadRequest("La cantidad debe ser al menos 1.");
        if (string.IsNullOrWhiteSpace(entidad.CodigoZapato) ||
            string.IsNullOrWhiteSpace(entidad.DocumentoCliente))
            return BadRequest("Debe indicar el código del zapato y el documento del cliente.");

        var zapato = conexion.Zapatos.FirstOrDefault(z => z.Codigo == entidad.CodigoZapato);
        if (zapato is null)
            return NotFound("No existe un zapato con ese código.");

        var clienteExiste = conexion.Clientes.Any(c => c.Documento == entidad.DocumentoCliente);
        if (!clienteExiste)
            return NotFound("No existe un cliente con ese documento.");

        if (entidad.Fecha == default)
            entidad.Fecha = DateTime.Today;

        var porcentaje = Descuentos.ObtenerPorcentaje(entidad.Cantidad, entidad.Fecha);
        var subtotal = zapato.Precio * entidad.Cantidad;
        var valorDescuento = Math.Round(subtotal * porcentaje, 2);
        var total = Math.Round(subtotal - valorDescuento, 2);

        var ventaGuardada = aplicacion.Insertar(entidad);
        return Ok(new ResultadoVenta(
            ventaGuardada, zapato.Precio, subtotal, porcentaje, valorDescuento, total));
    }

    [HttpPost]
    public Venta Actualizar(Venta entidad) => aplicacion.Actualizar(entidad);

    [HttpDelete("{id:int}")]
    public Venta Borrar(int id) => aplicacion.Borrar(id);
}
