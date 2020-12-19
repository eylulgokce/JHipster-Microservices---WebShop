using MicroserviceCommon.Clients;
using MicroserviceCommon.Clients.Interfaces;
using MicroserviceCommon.Configuration;
using MicroserviceCommon.Messages;
using MicroserviceCommon.Messages.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

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
            services.AddSingleton<IMessagePublisher, RabbitMessagePublisher>();

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

            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("notifications", type: ExchangeType.Direct);

                var queueName = channel.QueueDeclare().QueueName;
                channel.QueueBind(queue: queueName,
                                  exchange: "notifications",
                                  routingKey: "xyz");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine(message);
                };

                channel.BasicConsume(queue: queueName,
                                     autoAck: true,
                                     consumer: consumer);
            }
        }
    }
}
