using Fruits.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fruits.App.Controllers
{
    public abstract class BaseController : Controller
    {
        private readonly INotificator _notificator;

        protected BaseController(INotificator notificador)
        {
            _notificator = notificador;
        }

        public bool IsValid()
        {
            return !_notificator.HasNotifications();
        }
    }
}