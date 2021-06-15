using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.Helper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EnergyMarketApi
{
    public class Startup
    {
        private readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(opts =>
            {
                opts.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

            services.AddControllers();
            AddDependencyInjection(ref services);
        }

        private void AddDependencyInjection(ref IServiceCollection services)
        {
            services.AddTransient<MarketLogic>();
            services.AddTransient<JwtLogic>();
            services.AddTransient<BuyConsumer>();
            services.AddTransient<SellConsumer>();
            services.AddSingleton(service => AutoMapperConfig.Config.CreateMapper());
            services.AddSingleton(service => new RabbitMqChannel().GetChannel());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            new List<IConsumer>
            {
                app.ApplicationServices.GetService<BuyConsumer>(),
                app.ApplicationServices.GetService<SellConsumer>()
            }.ForEach(consumer => consumer.Consume());

            app.UseRouting();
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
            });
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
