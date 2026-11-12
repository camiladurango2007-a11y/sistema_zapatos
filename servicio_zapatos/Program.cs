using CD_lib_zapatos.implementaciones;
using CD_lib_zapatos.interfaces;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddCors(options => options.AddPolicy("FrontZapatos", policy =>
    policy.WithOrigins("http://localhost:5179", "https://localhost:7179")
          .AllowAnyHeader().AllowAnyMethod()));
builder.Services.AddScoped<IConexion, Conexion>();
builder.Services.AddScoped(typeof(IEntidadAplicacion<>), typeof(EntidadAplicacion<>));
var app = builder.Build();
app.UseCors("FrontZapatos");
app.MapControllers();
app.MapGet("/", () => "Servicio de CD_lib_zapatos activo");
app.Run();
