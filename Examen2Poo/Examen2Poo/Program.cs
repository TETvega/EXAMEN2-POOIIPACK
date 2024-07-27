using Examen2Poo.API;
using Examen2Poo.Database;
using Microsoft.AspNetCore.Hosting;

var builder = WebApplication.CreateBuilder(args);

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);


var app = builder.Build();

startup.Configure(app, app.Environment);
// momento donde estan cargados todos los servicios
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<Examen2PooContext>();
        await Examen2PooSeeder.LoasDataAsync(context, loggerFactory);

    }
    catch (Exception e)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(e, "Error ejecutar Seed de Datos");
    }


}

app.Run();
