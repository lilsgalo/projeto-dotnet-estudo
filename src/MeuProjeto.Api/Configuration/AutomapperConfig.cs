using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Business.Models;
using MeuProjeto.Business.Models.Identity;
using System;
using System.Linq;

namespace MeuProjeto.Api.Configuration
{
    public class AutomapperConfig : AutoMapper.Profile
    {
        public AutomapperConfig()
        {
            CreateMap(typeof(IPagedList<>), typeof(IPagedList<>));

            CreateMap<string, Text>()
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src));
            CreateMap<Text, string>()
                .ConvertUsing(src => src.Value);

            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<ChangeUserPasswordViewModel, User>().ReverseMap();
            CreateMap<UpdateUserViewModel, UpdateUser>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<CreateUserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<UpdateUserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<PermissionViewModel, Permission>().ReverseMap();

            #region Settings
            CreateMap<Settings, SettingsViewModel>();
            CreateMap<SettingsUpdateViewModel, Settings>();
            CreateMap<Settings, SimpleItemViewModel>();
            #endregion

            #region UserProfile
            CreateMap<CreateUserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<SimpleItemViewModel, UserProfile>().ReverseMap();
            CreateMap<UserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<UpdateUserProfileViewModel, UserProfile>().ReverseMap();
            CreateMap<UserProfile, CustomRole>();
            CreateMap<CustomRole, UserProfile>()
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Claims));
            CreateMap<CustomRole, UserProfileViewModel>()
                .ForMember(dest => dest.HasRelations, opt => opt.MapFrom(src =>
                    src.UserRoles.Any(p => p.User.Deleted == false)))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Claims));
            #endregion

            #region User
            CreateMap<User, CustomUser>()
                .ForPath(dest => dest.Icon, opt => opt.MapFrom(src => new Picture() { Id = Guid.NewGuid(), Value = src.Icon }))
                .ForPath(dest => dest.Image, opt => opt.MapFrom(src => new Picture() { Id = Guid.NewGuid(), Value = src.Image }));
            CreateMap<CustomUser, User>()
                .ForMember(dest => dest.ProfileId, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().RoleId))
                .ForMember(dest => dest.Profile, opt => opt.MapFrom(src => src.UserRoles.FirstOrDefault().Role))
                .ForMember(dest => dest.Permissions, opt => opt.MapFrom(src => src.Claims))
                .ForMember(dest => dest.Image, opt => opt.MapFrom(src => src.Image == null ? "" : src.Image.Value))
                .ForMember(dest => dest.Icon, opt => opt.MapFrom(src => src.Icon == null ? "" : src.Icon.Value));
            CreateMap<UpdateUser, CustomUser>()
                .ForPath(dest => dest.Icon.Value, opt => opt.MapFrom(src => src.Icon))
                .ForPath(dest => dest.Image.Value, opt => opt.MapFrom(src => src.Image));
            CreateMap<CreateUserViewModel, User>().ReverseMap();
            CreateMap<SimpleItemViewModel, User>().ReverseMap();
            CreateMap<ChangeUserPasswordViewModel, User>().ReverseMap();
            CreateMap<UpdateUserViewModel, UpdateUser>().ReverseMap();
            CreateMap<User, UpdateUser>().ReverseMap();
            CreateMap<UserViewModel, User>().ReverseMap();
            CreateMap<User, UserCurrentUserViewModel>().ReverseMap();
            CreateMap<UserForUpdateViewModel, User>().ReverseMap();
            CreateMap<CustomUser, BasicUserViewModel>().ReverseMap();
            CreateMap<DefaultUser, User>();
            CreateMap<CustomUser, CustomUser>().ReverseMap();
            #endregion

            #region Permission
            CreateMap<PermissionViewModel, Permission>().ReverseMap();
            CreateMap<Permission, CustomUserClaim>()
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));
            CreateMap<CustomUserClaim, Permission>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            CreateMap<Permission, CustomRoleClaim>()
                .ForMember(dest => dest.ClaimType, opt => opt.MapFrom(src => src.Type))
                .ForMember(dest => dest.ClaimValue, opt => opt.MapFrom(src => src.Value))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => 0));
            CreateMap<CustomRoleClaim, Permission>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.ClaimType))
                .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.ClaimValue))
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.NewGuid()));
            #endregion

            #region UserManual
            CreateMap<UserManual, UserManualViewModel>()
                .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content != null ? src.Content.Value : ""))
                .ForMember(dest => dest.LastReviewer, opt => opt.MapFrom(src => src.LastReviewer != null ? src.LastReviewer.Name : ""));
            CreateMap<UserManual, UserManualListViewModel>()
                .ForMember(dest => dest.LastReviewer, opt => opt.MapFrom(src => src.LastReviewer != null ? src.LastReviewer.Name : ""));
            CreateMap<UpdateUserManualViewModel, UserManual>();
            #endregion

            #region Log
            CreateMap<Log, LogViewModel>()
                .AfterMap((src, dest) => dest.Message = src.Message.Split("|")[0])
                .AfterMap((src, dest) => dest.OldState = src.OldState.Length > 0 ? System.Text.Json.JsonSerializer.Deserialize<dynamic>(src.OldState) : "")
                .AfterMap((src, dest) => dest.NewState = src.NewState.Length > 0 ? System.Text.Json.JsonSerializer.Deserialize<dynamic>(src.NewState) : "")
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            CreateMap<Log, LogListViewModel>();
            #endregion

        }
    }
}