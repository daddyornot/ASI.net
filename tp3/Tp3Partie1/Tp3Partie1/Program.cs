using Microsoft.EntityFrameworkCore;
using Tp3Partie1.Models.DataManager;
using Tp3Partie1.Models.EntityFramework;
using Tp3Partie1.Models.Repository;

namespace Tp3Partie1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var myAllowSpecificOrigins = "_myAllowSpecificOrigins";
            var builder = WebApplication.CreateBuilder(args);


            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: myAllowSpecificOrigins,
                    policy =>
                    {
                        policy.WithOrigins("http://localhost:5101")
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            // Add services to the container.

            builder.Services.AddControllers().AddNewtonsoftJson();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // Add DbContext 
            builder.Services
                .AddDbContext<SeriesDbContext>(options =>
                    options.UseNpgsql(builder.Configuration.GetConnectionString("SeriesDbContext"))
                );

            // Add Repository
            builder.Services.AddScoped<IDataRepository<Utilisateur>, UtilisateurManager>();

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
            app.UseCors(myAllowSpecificOrigins);

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}