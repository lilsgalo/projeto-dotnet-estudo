using FluentValidation;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Business.Models.Validations
{
    public class UpdateUserValidation : AbstractValidator<UpdateUser>
    {
        public UpdateUserValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted)
                .Length(2, 200).WithMessage(GeneralResources.Validations.Length.Formatted).WithName(UserResources.Name.Key);

            RuleFor(c => c.Email)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted)
                .Length(2, 100).WithMessage(GeneralResources.Validations.Length.Formatted).WithName(UserResources.Email.Key);

            RuleFor(c => c.PhoneNumber)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted)
                .Length(8, 13).WithMessage(GeneralResources.Validations.Length.Formatted).WithName(UserResources.PhoneNumber.Key);

        }
    }
}