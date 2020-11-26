using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Database
{
    public class InMemoryNotificationDatabase : INotificationDatabase
    {
        private List<Notification> AllNotifications;
        public void AddNotification(Notification notification)
        {
            AllNotifications.Add(notification);
        }

        public void DismissNotification(int idNotification)
        {
            AllNotifications.RemoveAt(idNotification);
        }

        public List<Notification> GetAllNotifications()
        {
            return AllNotifications;
        }
    }
}
