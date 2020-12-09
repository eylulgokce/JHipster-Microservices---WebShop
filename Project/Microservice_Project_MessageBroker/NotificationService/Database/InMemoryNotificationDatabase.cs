using Microsoft.Extensions.Logging;
using NotificationService.Model;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

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
            var assignedId = _nextId++;
            notification.IdNotification = assignedId;
            _allNotifications.TryAdd(assignedId, notification);
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
