using Newtonsoft.Json;
using presentaciones_zapatos.interfaces;
using System.Text;

namespace presentaciones_zapatos.implementaciones;

public class Comunicaciones : IComunicaciones
{
    private readonly HttpClient cliente;
    public Comunicaciones(HttpClient cliente) => this.cliente = cliente;
    public async Task<T> Ejecutar<T>(string url, object? entidad)
    {
        var body = new StringContent(JsonConvert.SerializeObject(entidad), Encoding.UTF8, "application/json");
        using var response = await cliente.PostAsync(url, body);
        response.EnsureSuccessStatusCode();
        var json = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<T>(json)!;
    }
}
