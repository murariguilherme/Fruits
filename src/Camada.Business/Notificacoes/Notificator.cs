using System.Collections.Generic;
using System.Linq;
using Fruits.Business.Interfaces;

namespace Fruits.Business.Notificacoes
{
    public class Notificator: INotificator
    {
        private List<Notification> notifications;

        public Notificator()
        {
            notifications = new List<Notification>();
        }

        public bool HasNotifications()
        {
            return notifications.Any();
        }

        public List<Notification> ListAllNotifications()
        {
            return notifications;
        }

        public void Handle(Notification notification)
        {
            notifications.Add(notification);
        }
    }
}