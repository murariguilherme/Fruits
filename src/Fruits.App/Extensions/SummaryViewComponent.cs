using System.Threading.Tasks;
using Fruits.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fruits.App.Extensions
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotificator _notificator;
        public SummaryViewComponent(INotificator notificador)
        {
            _notificator = notificador;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var notifications = await Task.FromResult(_notificator.ListAllNotifications());

            notifications.ForEach(n => ViewData.ModelState.AddModelError(string.Empty, n.Message));

            return View();
        }
    }
}