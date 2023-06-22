using MeuProjeto.Business.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MeuProjeto.Api.ViewModels
{
    public class UserManualViewModel
    {
        [Key]
        public Guid Id { get; set; }

        public string Code { get; set; }

        public int Revision { get; set; }
        public DateTime ReviewDate { get; set; }
        public string Content { get; set; }
        public Guid LastReviewerId { get; set; }
        public string LastReviewer { get; set; }
    }

    public class UserManualListViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Code { get; set; }
        public int Revision { get; set; }
        public DateTime ReviewDate { get; set; }
        public string LastReviewer { get; set; }
    }

    public class UpdateUserManualViewModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessageResourceType = typeof(GeneralResources.Validations.Required), ErrorMessageResourceName = "Formatted")]
        [Display(Name = "Key", ResourceType = typeof(UserManualResources.Content))]
        public string Content { get; set; }
    }
}