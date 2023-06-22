using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;

namespace MeuProjeto.Business.Services
{
    public abstract class IdentityBaseService<TEntity>
    {
        protected INotifier _notifier;
        protected IdentityBaseService(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
                Notify(error.ErrorMessage);
        }

        protected void Notify(Notification notification)
        {
            _notifier.Handle(notification);
        }

        protected void Notify(string mensagem)
        {
            Notify(new Notification(mensagem));
        }

        protected bool RunValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

    }
}