using Microsoft.EntityFrameworkCore;
using Tp3Partie1.Models.EntityFramework;

namespace Tp3Partie1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext 
            builder.Services
                .AddDbContext<SeriesDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("SeriesDbContext"))
                );
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                var context = services.GetRequiredService<SeriesDbContext>();
                context.Database.EnsureCreated();
                // DbInitializer.Initialize(context);
            }
            
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}