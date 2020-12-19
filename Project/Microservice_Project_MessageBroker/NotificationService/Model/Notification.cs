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

        [DataMember(Name ="idNotification")]
        public int IdNotification { get; set; }
        [DataMember(Name = "level")]
        public string Level { get; set; }
        [DataMember(Name = "message")]
        public string Message { get; set; }
    }
}
