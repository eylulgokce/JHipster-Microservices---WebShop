using System.Collections.Generic;

namespace NotificationService.Model
{
    public class Notification
    {
        public Notification(string level, string message)
        {
            Level = level;
            Message = message;
        }

        public string Level { get; set; }
        public string Message { get; set; }
    }
}
