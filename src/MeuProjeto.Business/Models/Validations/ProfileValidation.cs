using FluentValidation;
using MeuProjeto.Business.DTOs;

namespace MeuProjeto.Business.Models.Validations
{
    public class ProfileValidation : AbstractValidator<UserProfile>
    {
        public ProfileValidation()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres");

        }
    }
}