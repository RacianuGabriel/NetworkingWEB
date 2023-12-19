using Microsoft.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();

            using var scope = host.Services.CreateScope();

            var services=scope.ServiceProvider;

            try{
                var context=services.GetRequiredService<DataContext>();
                await context.Database.MigrateAsync();
                await Seed.SeedData(context);
            }catch(Exception ex){
                var logger =services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex,"An error occured during migration");
            }

            await host.RunAsync();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}