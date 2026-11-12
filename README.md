# sistema_zapatos

Solución para la gestión de zapatos, clientes y ventas.

## Proyectos
- `CD_lib_zapatos`: biblioteca de clases con las entidades, interfaces, implementaciones, conexión y reglas de negocio.
- `servicio_zapatos`: API para operaciones sobre zapatos, clientes y ventas.
- `presentaciones_zapatos`: biblioteca de presentación/comunicaciones.
- `front_zapatos`: aplicación web.
- `pruebas_zapatos`: pruebas unitarias.

## Descuentos en ventas
Al registrar una venta mediante `POST /Ventas/Insertar`, el servicio busca el zapato y el cliente, calcula el subtotal y aplica un único descuento con esta prioridad:
1. Más de 4 pares: 14 %.
2. Temporada escolar (configurada provisionalmente como enero y febrero): 8,4 %.
3. Final de año (configurado provisionalmente como noviembre y diciembre): 5 %.

La respuesta incluye subtotal, porcentaje de descuento, valor descontado y total. La entidad `Venta` conserva sus cinco atributos; el resumen es un DTO de respuesta y no una tabla adicional.

## Compilar
Desde la carpeta que contiene `sistema_zapatos.slnx`:

```bash
dotnet restore
dotnet build
```

Ejecutar el script `CD_lib_zapatos/Script.sql` en SQL Server antes de probar la API. Revisar la cadena de conexión en `CD_lib_zapatos/nucleo/DatosGenerales.cs`.

> Los meses de temporada escolar y fin de año son supuestos operativos porque el enunciado de la fotografía no especifica meses concretos.


## Frontend y Postman

1. Inicia la API: `dotnet run --project servicio_zapatos/servicio_zapatos.csproj` (http://localhost:5209).
2. Inicia el frontend: `dotnet run --project front_zapatos/front_zapatos.csproj` (http://localhost:5179).
3. En Postman importa `postman/sistema_zapatos.postman_collection.json` y `postman/sistema_zapatos.postman_environment.json`; selecciona el entorno Local.
4. Para probar ventas, registra primero un zapato y un cliente. Las peticiones de venta devuelven subtotal, porcentaje, valor del descuento y total.

La interfaz web consume la API mediante JavaScript y la API permite CORS desde localhost:5179.
