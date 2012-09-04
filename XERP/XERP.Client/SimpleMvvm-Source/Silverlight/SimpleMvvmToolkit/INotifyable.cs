using System;

namespace SimpleMvvmToolkit
{
    public interface INotifyable
    {
        void Notify(string token, object sender, NotificationEventArgs e);
    }
}
