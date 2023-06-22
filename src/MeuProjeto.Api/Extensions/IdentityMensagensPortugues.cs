using MeuProjeto.Business.Resources;
using Microsoft.AspNetCore.Identity;

namespace MeuProjeto.Api.Extensions
{
    public class IdentityMensagensPortugues : IdentityErrorDescriber
    {
        public override IdentityError DefaultError() { return new IdentityError { Code = nameof(DefaultError), Description = GeneralResources.Errors.DefaultError.Formatted }; }
        public override IdentityError ConcurrencyFailure() { return new IdentityError { Code = nameof(ConcurrencyFailure), Description = AuthResources.Errors.ConcurrencyFailure.Formatted }; }
        public override IdentityError PasswordMismatch() { return new IdentityError { Code = nameof(PasswordMismatch), Description = AuthResources.Errors.PasswordMismatch.Formatted }; }
        public override IdentityError InvalidToken() { return new IdentityError { Code = nameof(InvalidToken), Description = AuthResources.Errors.InvalidToken.Formatted }; }
        public override IdentityError LoginAlreadyAssociated() { return new IdentityError { Code = nameof(LoginAlreadyAssociated), Description = UserResources.Errors.LoginAlreadyAssociated.Formatted }; }
        public override IdentityError InvalidUserName(string userName) { return new IdentityError { Code = nameof(InvalidUserName), Description = string.Format(UserResources.Errors.InvalidUserName.Formatted, userName) }; }
        public override IdentityError InvalidEmail(string email) { return new IdentityError { Code = nameof(InvalidEmail), Description = string.Format(UserResources.Errors.InvalidEmail.Formatted, email) }; }
        public override IdentityError DuplicateUserName(string userName) { return new IdentityError { Code = nameof(DuplicateUserName), Description = string.Format(UserResources.Errors.DuplicateUserName.Formatted, userName) }; }
        public override IdentityError DuplicateEmail(string email) { return new IdentityError { Code = nameof(DuplicateEmail), Description = string.Format(UserResources.Errors.DuplicateEmail.Formatted, email) }; }
        public override IdentityError InvalidRoleName(string role) { return new IdentityError { Code = nameof(InvalidRoleName), Description = string.Format(UserResources.Errors.InvalidRoleName.Formatted, role) }; }
        public override IdentityError DuplicateRoleName(string role) { return new IdentityError { Code = nameof(DuplicateRoleName), Description = string.Format(UserResources.Errors.DuplicateRoleName.Formatted, role) }; }
        public override IdentityError UserAlreadyHasPassword() { return new IdentityError { Code = nameof(UserAlreadyHasPassword), Description = UserResources.Errors.UserAlreadyHasPassword.Formatted }; }
        public override IdentityError UserLockoutNotEnabled() { return new IdentityError { Code = nameof(UserLockoutNotEnabled), Description = AuthResources.Errors.UserLockoutNotEnabled.Formatted }; }
        public override IdentityError UserAlreadyInRole(string role) { return new IdentityError { Code = nameof(UserAlreadyInRole), Description = string.Format(UserResources.Errors.UserAlreadyInRole.Formatted, role) }; }
        public override IdentityError UserNotInRole(string role) { return new IdentityError { Code = nameof(UserNotInRole), Description = string.Format(UserResources.Errors.UserNotInRole.Formatted, role) }; }
        public override IdentityError PasswordTooShort(int length) { return new IdentityError { Code = nameof(PasswordTooShort), Description = string.Format(UserResources.Errors.PasswordTooShort.Formatted, length.ToString()) }; }
        public override IdentityError PasswordRequiresNonAlphanumeric() { return new IdentityError { Code = nameof(PasswordRequiresNonAlphanumeric), Description = UserResources.Errors.PasswordRequiresNonAlphanumeric.Formatted }; }
        public override IdentityError PasswordRequiresDigit() { return new IdentityError { Code = nameof(PasswordRequiresDigit), Description = UserResources.Errors.PasswordRequiresDigit.Formatted }; }
        public override IdentityError PasswordRequiresLower() { return new IdentityError { Code = nameof(PasswordRequiresLower), Description = UserResources.Errors.PasswordRequiresLower.Formatted }; }
        public override IdentityError PasswordRequiresUpper() { return new IdentityError { Code = nameof(PasswordRequiresUpper), Description = UserResources.Errors.PasswordRequiresUpper.Formatted }; }
    }
}