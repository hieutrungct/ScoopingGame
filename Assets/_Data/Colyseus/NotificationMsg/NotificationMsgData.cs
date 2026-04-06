namespace Rubik.NotificationMsg
{

    public enum NotificationMsgType
    {
        MessageBanner = 1,
        WarningBanner = 2,
        ErrorBanner = 3,
        MessagePopup = 4,
    }

    public class NotificationMessage
    {
        public string Message;
        public string[] Data;
        public int TimeShow = 3; // seconds
    }

    public class NotificationPopup {
        public string Title;
        public string Message;
        public string[] Data;
        public bool ForceLogout = false;
    }

    public class NotificationMsg
    {
        public string _id;
        public NotificationMsgType Type;
        public string Data;
    }

}