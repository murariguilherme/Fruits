using Fruits.Business.Interfaces;
using Fruits.Business.Models;
using Fruits.Business.Notificacoes;
using FluentValidation;
using FluentValidation.Results;

namespace Fruits.Business.Services
{
    public abstract class BaseService
    {
        protected readonly INotificator _notificator;

        protected BaseService(INotificator notificador)
        {
            _notificator = notificador;
        }

        public void Notificate(string error)
        {
            _notificator.Handle(new Notification(error));
        }

        public void Notificate(ValidationResult validationResult)
        {
            foreach (var validationResultError in validationResult.Errors)
            {
                Notificate(validationResultError.ErrorMessage);
            }
        }

        public bool ExecuteValidation<TV, TE>(TV validador, TE entidade)
            where TE : Entity where TV : AbstractValidator<TE>
        {
            var validationResult = validador.Validate(entidade);
            if (validationResult.IsValid) return true;
            Notificate(validationResult);
            return false;
        }
    }
}