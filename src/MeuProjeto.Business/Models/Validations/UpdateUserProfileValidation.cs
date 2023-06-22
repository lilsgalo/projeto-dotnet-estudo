using FluentValidation;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Business.Models.Validations
{
    public class UpdateUserProfileValidation : AbstractValidator<UserProfile>
    {
        public UpdateUserProfileValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted)
                .Length(2, 100).WithMessage(GeneralResources.Validations.Length.Formatted).WithName(UserProfileResources.Name.Key);

        }
    }
}