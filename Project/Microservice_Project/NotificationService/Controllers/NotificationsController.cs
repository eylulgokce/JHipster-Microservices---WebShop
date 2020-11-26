using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NotificationService.Database;
using NotificationService.Model;

namespace NotificationService.Controllers
{
    public class NotificationsController : Controller
    {
        private INotificationDatabase _notificationDatabse;
        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public List<Notification> GetAllNotifications()
        {
            return _notificationDatabse.GetAllNotifications();
        }

        [HttpPost]
        public void AddNotification([FromBody] string level, [FromBody] string message)
        {
            Notification notification = new Notification(level, message);
            _notificationDatabse.AddNotification(notification);
        }

        [HttpDelete("{idNotification}")]
        public void DismissNotification(int idNotification)
        {
            try
            {
                _notificationDatabse.DismissNotification(idNotification);
            }
            catch (Exception ex)
            {

            }

        }
    }
       
}
