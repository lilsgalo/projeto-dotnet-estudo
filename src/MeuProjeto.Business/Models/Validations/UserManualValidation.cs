using FluentValidation;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Business.Models.Validations
{
    public class UserManualValidation : AbstractValidator<UserManual>
    {
        public UserManualValidation()
        {
            When(f => f.Content != null, () =>
            {
                RuleFor(c => c.Content.Value)
                    .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted).WithName(UserManualResources.Content.Key);
            }).Otherwise(() => {
                RuleFor(c => c.Content)
                    .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted).WithName(UserManualResources.Content.Key);
            });
        }

    }
}
