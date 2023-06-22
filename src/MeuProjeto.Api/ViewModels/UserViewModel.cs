using DevIO.Api.Extensions;
using MeuProjeto.Business.Resources;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Api.ViewModels
{
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "user")]
    public class CreateUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.UserName))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [EmailAddress(ErrorMessageResourceType = typeof(GeneralResources.Validations.EmailAddress), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Email))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Password))]
        public string Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(GeneralResources.Validations.Compare), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.ConfirmPassword))]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(13, MinimumLength = 8, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Profile))]
        public Guid ProfileId { get; set; }

        public List<PermissionViewModel> Permissions { get; set; }
        //public IFormFile Image { get; set; }
    }
    [ModelBinder(typeof(JsonWithFilesFormDataModelBinder), Name = "user")]
    public class UpdateUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Id))]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [EmailAddress(ErrorMessageResourceType = typeof(GeneralResources.Validations.EmailAddress), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Email))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Name))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(13, MinimumLength = 8, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.PhoneNumber))]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Profile))]
        public Guid ProfileId { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
    public class ChangeUserPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Id))]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.NewPassword))]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceType = typeof(GeneralResources.Validations.Compare), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.ConfirmNewPassword))]
        public string ConfirmNewPassword { get; set; }
    }
    public class ChangeCurrentUserPasswordViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Password))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.NewPassword))]
        public string NewPassword { get; set; }

        [Compare("NewPassword", ErrorMessageResourceType = typeof(GeneralResources.Validations.Compare), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.ConfirmNewPassword))]
        public string ConfirmNewPassword { get; set; }
    }

    public class CreateUserProfileViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserProfileResources.Name))]
        public string Name { get; set; }

        public List<PermissionViewModel> Permissions { get; set; }

    }
    public class UpdateUserProfileViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserProfileResources.Id))]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 2, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserProfileResources.Name))]
        public string Name { get; set; }

        public List<PermissionViewModel> Permissions { get; set; }
    }

    public class LoginUserViewModel
    {
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.UserName))]
        public string UserName { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(100, MinimumLength = 6, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserResources.Password))]
        public string Password { get; set; }
    }
    public class UserTokenViewModel
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public IEnumerable<PermissionViewModel> Claims { get; set; }
    }
    public class LoginResponseViewModel
    {
        public string AccessToken { get; set; }
        public double ExpiresIn { get; set; }
        public UserTokenViewModel UserToken { get; set; }
    }

    public class UserProfileViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
        public bool HasRelations { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
    public class UserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool Active { get; set; }

        public SimpleItemViewModel Profile { get; set; }
        public Guid ProfileId { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
    public class BasicUserViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }
        public Guid ProfileId { get; set; }
    }
    public class UserForUpdateViewModel
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Active { get; set; }

        public string Image { get; set; }
        public Guid ProfileId { get; set; }
        public List<PermissionViewModel> Permissions { get; set; }
    }
    public class PermissionViewModel
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}