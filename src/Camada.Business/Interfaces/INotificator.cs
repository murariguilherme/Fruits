using System.Collections.Generic;
using Fruits.Business.Notificacoes;


namespace Fruits.Business.Interfaces
{
    public interface INotificator
    {
        bool HasNotifications();
        List<Notification> ListAllNotifications();
        void Handle(Notification notification);
    }
}