using Examen2Poo.API.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Examen2Poo.Database
{
    public class Examen2PooSeeder
    {

        public static async Task LoasDataAsync(
            // Obteniendo el contexto de la base dd Datos
            Examen2PooContext context,
            ILoggerFactory loggerFactory
            )
        {
            try
            {
                // las que no dependen de nadie 

                // dependencias de nivel 1

                // las que dependen de las anteriores 

               // await LoadCategoriesAsync(loggerFactory, context);

            }
            catch (Exception e)
            {
                // creando un objeto logger y queda la huella de esta clase y dice el error digamos BlogUnahSeeder Error
                var logger = loggerFactory.CreateLogger<Examen2PooSeeder>();
                // mensaje de error que mostrara la api al cargar datos desde aqui
                logger.LogError(e, "Error inicializando la data del API");
            }


        }

        public static async Task LoadCategoriesAsync(
            ILoggerFactory loggerFactory,
            Examen2PooContext context
            )
        {
            try
            {
                //var jsonFilePath = "SendData/categories.json";
                //// leyendo y pasando a objeto de bits
                //var jsonContent = await File.ReadAllTextAsync(jsonFilePath);
                //// leyendo y convirtiendo a objeto de c#
                //var categories = JsonConvert.DeserializeObject<List<CategoryEntity>>(jsonContent);
                //if (!await context.TBL_Categories.AnyAsync())
                //{
                //    for (int i = 0; i < categories.Count; i++)
                //    {
                //        categories[i].CreatedBy = "4c902f38-7d0f-4016-a550-e55b7e96db38";
                //        categories[i].CreatedDate = DateTime.Now;
                //        categories[i].UpdateBy = "4c902f38-7d0f-4016-a550-e55b7e96db38";
                //        categories[i].UpdatedDate = DateTime.Now;
                //    }
                //    context.AddRange(categories);
                //    await context.SaveChangesAsync();
                //}

            }
            catch (Exception e)
            {
                var logger = loggerFactory.CreateLogger<Examen2PooSeeder>();
                // mensaje de error que mostrara la api al inyectar datos desde aqui
                logger.LogError(e, "Error al inyectar los datos desde el seed de categorias");
            }
        }
    }
}
