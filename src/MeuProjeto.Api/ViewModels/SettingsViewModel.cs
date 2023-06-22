using MeuProjeto.Business.Enums;
using MeuProjeto.Business.Resources;
using System;
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Api.ViewModels
{

    public class SettingsViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public SettingsTypeEnum Type { get; set; }
        public string Code { get; set; }
        public string Contents { get; set; }
    }

    public class SettingsUpdateViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public SettingsTypeEnum Type { get; set; }
        
        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [StringLength(200, MinimumLength = 2, ErrorMessageResourceType = typeof(GeneralResources.Validations.StringLength), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(SettingsResources.Code))]
        public string Code { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(SettingsResources.Contents))]
        public dynamic Contents { get; set; }
    }
}