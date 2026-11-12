namespace CD_lib_zapatos.nucleo;

public static class Descuentos
{
    // Prioridad: compra de más de cuatro pares; luego temporada escolar; luego fin de año.
    public static decimal ObtenerPorcentaje(int cantidad, DateTime fecha)
    {
        if (cantidad > 4) return 0.14m;
        if (fecha.Month is 1 or 2) return 0.084m;
        if (fecha.Month is 11 or 12) return 0.05m;
        return 0m;
    }

    public static decimal CalcularTotal(decimal precioUnitario, int cantidad, DateTime fecha)
    {
        if (precioUnitario < 0 || cantidad < 1) throw new ArgumentException("Precio o cantidad no válidos.");
        var subtotal = precioUnitario * cantidad;
        return Math.Round(subtotal * (1m - ObtenerPorcentaje(cantidad, fecha)), 2);
    }
}
