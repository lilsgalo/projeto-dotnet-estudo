using FluentValidation;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Business.Models.Validations
{
    public class SettingsValidation : AbstractValidator<Settings>
    {
        public SettingsValidation()
        {
            RuleFor(c => c.Code)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted);

            RuleFor(c => c.Contents)
                .NotEmpty().WithMessage(GeneralResources.Validations.NotEmpty.Formatted).WithName(SettingsResources.Contents.Key);
           
        }
    }
}
