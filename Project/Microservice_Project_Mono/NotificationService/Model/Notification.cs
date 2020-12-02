using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NotificationService.Model
{
    [DataContract]
    public class Notification
    {
        public Notification() { }
        public Notification(int idNotification, string level, string message)
        {
            IdNotification = idNotification;
            Level = level;
            Message = message;
        }

        public int IdNotification { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }
}
