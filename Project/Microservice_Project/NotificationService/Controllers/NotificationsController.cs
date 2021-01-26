using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Database;
using NotificationService.Model;

namespace NotificationService.Controllers
{
    [ApiController]
    [Route("notifications")]
    public class NotificationsController : Controller
    {
        private readonly INotificationDatabase _notificationDatabase;

        public NotificationsController(INotificationDatabase notificationDatabase)
        {
            _notificationDatabase = notificationDatabase;
        }

        [HttpGet]
        public List<Notification> GetAllNotifications()
        {
            return _notificationDatabase.GetAllNotifications();
        }

        [HttpPost]
        public IActionResult AddNotification([FromBody] Notification notification)
        {
            _notificationDatabase.AddNotification(notification);
            return new OkResult();
        }

        [HttpDelete]
        public void DismissNotification([FromQuery] int idNotification)
        {
            try
            {
                _notificationDatabase.DismissNotification(idNotification);
            }
            catch (Exception ex)
            {
                
            }
        }
    }  
}
