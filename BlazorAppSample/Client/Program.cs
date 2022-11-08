using System;
using System.Net.Http;
using System.Threading.Tasks;
using BlazorAppSample.Models.Foundations.Ports;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syncfusion.Licensing;
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
            builder.Services.AddStandardEditViewService<Port>();
            builder.Services.AddStandardHttpClient(builder.Configuration);

            await builder.Build().RunAsync();
        }
    }
}