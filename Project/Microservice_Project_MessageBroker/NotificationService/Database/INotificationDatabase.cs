using NotificationService.Model;
using System.Collections.Generic;

namespace NotificationService.Database
{
    public interface INotificationDatabase
    {
        List<Notification> GetAllNotifications();
        void AddNotification(Notification notification);
        void DismissNotification(int idNotification);
    }
}
