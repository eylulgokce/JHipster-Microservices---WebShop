using CartService.Database;
using MicroserviceCommon.Clients;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CartService.Subscribers;

namespace CartService
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

            services.AddSingleton<INotificationsManager, NotificationsManager>();
            services.AddSingleton<ICartDatabase, CartDatabase>();
            services.AddSingleton<ICartBrokerClient, CartBrokerClientRabbitMQ>();
            services.AddSingleton<ICartExchangeSubscriber, CartExchangeSubscriber>();

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
            /*
            var factory = new ConnectionFactory() { HostName = "localhost" };
            //{
            //  Uri = new Uri("amqp://guest:quest@localhost:15672")
            //};
            
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.ExchangeDeclare("notifications", ExchangeType.Fanout);
            channel.QueueDeclare("test-queue",
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            channel.QueueBind("test-queue", "notifications", string.Empty, null);

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (sender, eventArguments) =>
            {
                var body = eventArguments.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine(message);
            };

            channel.BasicConsume("test-queue", true, consumer);*/

            app.ApplicationServices.GetService<ICartExchangeSubscriber>();
        }
    }
}
