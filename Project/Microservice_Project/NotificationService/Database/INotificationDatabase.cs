using NotificationService.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NotificationService.Database
{
    interface INotificationDatabase
    {
        List<Notification> GetAllNotifications();
        void AddNotification(Notification notification);
        void DismissNotification(int idNotification);
    }
}
