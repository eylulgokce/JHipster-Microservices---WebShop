﻿using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NotificationService.Model;
using RabbitMQ.Client;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MicroserviceCommon.CommonModel.Cart;

namespace NotificationService.Database
{
    public class InMemoryNotificationDatabase : INotificationDatabase
    {
        private readonly ILogger _logger;
        private readonly ConcurrentDictionary<int, Notification> _allNotifications;
        private int _nextId = 1;
        
        public InMemoryNotificationDatabase(ILogger<InMemoryNotificationDatabase> logger)
        {
            _logger = logger;
            _allNotifications = new ConcurrentDictionary<int, Notification>();
        }
        
        public void AddNotification(Notification notification)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            var assignedId = _nextId++;
            notification.IdNotification = assignedId;

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare("notifications-exchange", ExchangeType.Fanout);
                var body = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(notification));
                channel.BasicPublish("notifications-exchange", string.Empty, null, body);
            }

            _logger.LogInformation($"Added notification with ID {assignedId}!");
        }

        public void DismissNotification(int idNotification)
        {
            if (_allNotifications.ContainsKey(idNotification))
            {
                _allNotifications.TryRemove(idNotification, out var _);
                _logger.LogInformation($"Dismissed notification with ID {idNotification}!");
            }
            else
            {
                _logger.LogWarning($"Tried to dismiss non-existing notification with ID {idNotification}, ignoring...");
            }
        }

        public List<Notification> GetAllNotifications()
        {
            return _allNotifications.Values.ToList();
        }
    }
}
