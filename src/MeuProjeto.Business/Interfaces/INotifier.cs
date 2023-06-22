using System.Collections.Generic;
using MeuProjeto.Business.Notifications;

namespace MeuProjeto.Business.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
        void Handle(string notification);
    }
}