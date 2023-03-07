using System.Threading.Tasks;
using BlazorAppSample.Models.Foundations.Ports;
using BlazorAppSample.Models.Foundations.Users;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using TheStandardBox.UIKit.Blazor.Extensions;

namespace BlazorAppSample
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            builder.Services.AddTheStandardBox();
            builder.Services.AddFoundationService<Port>();
            builder.Services.AddFoundationService<User>();
            builder.Services.AddStandardEditViewService<Port>();
            builder.Services.AddStandardEditViewService<User>();
            builder.Services.AddStandardHttpClient(builder.Configuration);

            await builder.Build().RunAsync();
        }
    }
}