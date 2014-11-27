using System;
using Windows.ApplicationModel.Background;
using Windows.Data.Xml.Dom;
using Windows.UI.Notifications;

namespace IPlayer_BackgroundTask
{
    public sealed class Task:IBackgroundTask
    {
        public void Run(IBackgroundTaskInstance taskInstance)
        {
            XmlDocument xc = ToastNotificationManager.GetTemplateContent(ToastTemplateType.ToastText01);
            XmlNodeList xnl = xc.GetElementsByTagName("text");
            xnl[0].InnerText = "\nMetro Player is Waiting for You";
            var s = new ToastNotification(xc);
            s.ExpirationTime = DateTimeOffset.UtcNow.AddSeconds(30);
            ToastNotificationManager.CreateToastNotifier().Show(s);
        }
    }
}
