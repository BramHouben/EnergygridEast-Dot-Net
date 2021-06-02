using System.Collections.Generic;
using System.Data;
using EnergyMarketApi.Dal;
using EnergyMarketApi.Dal.Interface;
using EnergyMarketApi.Logic;
using EnergyMarketApi.Model.Helper;
using EnergyMarketApi.RabbitMq;
using EnergyMarketApi.RabbitMq.Consumers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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
            string connectionString = _config.GetConnectionString("DefaultConnection");
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new NoNullAllowedException();
            }

            services.AddDbContextPool<DataContext>(
                dbContextOptions => dbContextOptions
                    .UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

            services.AddControllers();
            AddDependencyInjection(ref services);
        }

        private void AddDependencyInjection(ref IServiceCollection services)
        {
            services.AddSingleton(service => new RabbitMqChannel().GetChannel());
            services.AddTransient<MarketLogic>();
            services.AddTransient<BuyConsumer>();
            services.AddTransient<SellConsumer>();
            services.AddTransient<IEnergyHistoryDal, EnergyHistoryDal>();
            services.AddSingleton(service => AutoMapperConfig.Config.CreateMapper());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            new List<IConsumer>
            {
                app.ApplicationServices.GetService<BuyConsumer>(),
                app.ApplicationServices.GetService<SellConsumer>()
            }.ForEach(consumer => consumer.Consume());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
