using BlazorApp1.Models;
using BlazorApp1.Services;
using BlazorApp1.ViewModel;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

namespace BlazorApp1
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddScoped<IService<Utilisateur>, WSServiceUtilisateur>();
            builder.Services.AddScoped<AllUsersViewModel>();
            builder.Services.AddScoped<AddUserViewModel>();
            builder.Services.AddScoped<EditUserViewModel>();
            builder.Services.AddScoped<UserSearchViewModel>();
            builder.Services.AddBlazorBootstrap(); 

            await builder.Build().RunAsync();
        }
    }
}