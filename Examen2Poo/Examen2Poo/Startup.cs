using Examen2Poo.API.Database;
using Examen2Poo.API.Helpers;
using Examen2Poo.API.Services;
using Examen2Poo.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Examen2Poo.Database;
using Examen2Poo.Services.Interfaces;
using Examen2Poo.Services;

namespace Examen2Poo.API
{
    public class Startup
    {
        private  IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration )
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers(); // para que reconozca todos los controladores para ser expuestos como recursos
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // add DbContext
            services.AddDbContext<Examen2PooContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            // add custom services
            services.AddTransient<IAuthService,AuthService>();//Servicios de Json
            services.AddTransient<IClientService, ClientService>();

            // add automapper
            services.AddAutoMapper(typeof(AutoMapperProfile));   

        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            /// trata de forzar a utilizar siempre https
            app.UseHttpsRedirection();


            app.UseRouting();// donde podemos definir rutas 

            // vallidadcion de autorizacion 
            app.UseAuthorization();


            app.UseEndpoints(endpoint =>
            {
                endpoint.MapControllers();
            });

        }

    }
}
