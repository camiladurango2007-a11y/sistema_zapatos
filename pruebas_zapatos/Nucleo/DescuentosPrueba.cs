using CD_lib_zapatos.nucleo;
namespace pruebas_zapatos.Nucleo;
[TestClass]
public class DescuentosPrueba
{
    [TestMethod] public void CompraMayorACuatroTieneCatorcePorCiento() => Assert.AreEqual(0.14m, Descuentos.ObtenerPorcentaje(5, new DateTime(2026, 6, 10)));
    [TestMethod] public void TemporadaEscolarTieneOchoComaCuatroPorCiento() => Assert.AreEqual(0.084m, Descuentos.ObtenerPorcentaje(1, new DateTime(2026, 1, 10)));
    [TestMethod] public void FinDeAnioTieneCincoPorCiento() => Assert.AreEqual(0.05m, Descuentos.ObtenerPorcentaje(1, new DateTime(2026, 12, 10)));
}
