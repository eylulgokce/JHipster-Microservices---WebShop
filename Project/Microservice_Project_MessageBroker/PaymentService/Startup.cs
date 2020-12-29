using MicroserviceCommon.Clients;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PaymentService.Database;
using PaymentService.Subscribers;

namespace PaymentService
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<INotificationsManager, NotificationsManager>();
            services.AddSingleton<IPaymentBrokerClient, PaymentBrokerClientRabbitMQ>();

            services.AddSingleton<IPaymentDatabase, PaymentDatabaseMySQL>();
            services.AddSingleton<IPaymentExchangeSubscriber, PaymentExchangeSubscriber>();

            var notificationConfigurationSection = Configuration.GetSection(NotificationConfiguration.SectionName);
            services.Configure<NotificationConfiguration>(notificationConfigurationSection);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
