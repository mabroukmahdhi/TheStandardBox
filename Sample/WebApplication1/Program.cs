using FluentAssertions.Common;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using TheStandardBox.Data.Controllers.Conventions;
using TheStandardBox.Data.Controllers.Providers;
using TheStandardBox.Data.Extensions;
using WebApplication1.Brokers.Storages;
using WebApplication1.Models.Foundations.Options;
using WebApplication1.Models.Foundations.UserOptions;
using WebApplication1.Models.Foundations.Users;

namespace WebApplication1
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers(options =>
            {
                options.Conventions.Add(new GenericControllerRouteConvention());
                options.Conventions.Add(new ActionBuildingConvention());
            }).ConfigureApplicationPartManager(m =>
            {
                m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider());
            });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddTheStandardBoxData<StorageBroker>();
            builder.Services.AddStandardFoundationService<Option>();
            builder.Services.AddStandardFoundationService<User>();
            builder.Services.AddStandardFoundationService<UserOption>();

            builder.Services.AddMvc()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                });

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}