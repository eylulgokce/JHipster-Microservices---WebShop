using NotificationService.Model;
using System.Collections.Generic;
using System.Linq;

namespace NotificationService.Database
{
    public class InMemoryNotificationDatabase : INotificationDatabase
    {
        private readonly Dictionary<int, Notification> _allNotifications;
        private int _nextId = 1;
        
        public InMemoryNotificationDatabase()
        {
            _allNotifications = new Dictionary<int, Notification>();
        }
        
        public void AddNotification(Notification notification)
        {
            var assignedId = _nextId++;
            notification.IdNotification = assignedId;
            _allNotifications.Add(assignedId, notification);
        }

        public void DismissNotification(int idNotification)
        {
            _allNotifications.Remove(idNotification);
        }

        public List<Notification> GetAllNotifications()
        {
            return _allNotifications.Values.ToList();
        }
    }
}
