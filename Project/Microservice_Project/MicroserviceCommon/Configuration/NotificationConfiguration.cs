namespace MicroserviceCommon.Configuration
{
    public class NotificationConfiguration
    {
        public const string SectionName = "NotificationService";

        public NotificationConfiguration() { }

        public string Endpoint { get; set; }// = "http://localhost:5002";
    }
}
