using MeuProjeto.Business.Interfaces;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Notifications;
using FluentValidation;
using FluentValidation.Results;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MeuProjeto.Business.Services
{
    public abstract class BaseService<TEntity> : IService<TEntity> where TEntity : Entity
    {
        private readonly INotifier _notifier;
        private readonly IRepository<TEntity> _repository;
        protected BaseService(INotifier notifier, IRepository<TEntity> repository)
        {
            _notifier = notifier;
            _repository = repository;
        }

        protected void Notify(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Notify(error.ErrorMessage);
            }
        }
        protected void Notify(string message)
        {
            Notify(new Notification(message));
        }

        protected void Notify(Notification notification)
        {
            _notifier.Handle(notification);
        }

        protected bool ValidOperation()
        {
            return !_notifier.HasNotification();
        }

        protected bool RunValidation<TV, TE>(TV validacao, TE entidade) where TV : AbstractValidator<TE>
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            Notify(validator);

            return false;
        }

        public virtual void Dispose()
        {
            _repository?.Dispose();
        }
    }
}