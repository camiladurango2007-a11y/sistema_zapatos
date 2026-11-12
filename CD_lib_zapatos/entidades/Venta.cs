namespace CD_lib_zapatos.entidades;

public class Venta
{
    public int Id { get; set; }
    public string? CodigoZapato { get; set; }
    public string? DocumentoCliente { get; set; }
    public DateTime Fecha { get; set; }
    public int Cantidad { get; set; }
}
