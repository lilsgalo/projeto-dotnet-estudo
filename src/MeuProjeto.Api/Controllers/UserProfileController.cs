using System.Threading.Tasks;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using System;
using MeuProjeto.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using MeuProjeto.Business.Resources;

namespace MeuProjeto.Api.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user-profiles")]
    public class UserProfileController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserProfileService _userProfileService;

        public UserProfileController(INotifier notificador,
                              IUserProfileService userProfileService,
                              IMapper mapper,
                              IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _userProfileService = userProfileService;
        }

        [ClaimsAuthorize(nameof(UserProfile), nameof(Create))]
        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateUserProfileViewModel createUserProfileViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userProfile = _mapper.Map<UserProfile>(createUserProfileViewModel);

            await _userProfileService.Create(userProfile);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(UserProfile), nameof(Update))]
        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateUserProfileViewModel updateUserProfileViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userProfile = await _userProfileService.GetById(updateUserProfileViewModel.Id);

            _mapper.Map(updateUserProfileViewModel, userProfile);

            await _userProfileService.Update(userProfile);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(UserProfile), nameof(Delete))]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userProfileService.Delete(id);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(UserProfile), nameof(Deactivate))]
        [HttpPatch("activate/{id}")]
        public async Task<ActionResult> Activate(Guid id)
        {
            await _userProfileService.Activate(id);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(UserProfile), nameof(Deactivate))]
        [HttpPatch("deactivate/{id}")]
        public async Task<ActionResult> Deactivate(Guid id)
        {
            await _userProfileService.Deactivate(id);

            return CustomResponse();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserProfileViewModel>> GetById(Guid id)
        {
            var userProfile = await _userProfileService.GetById(id);

            return CustomResponse(_mapper.Map<UserProfileViewModel>(userProfile));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<UserProfileViewModel>> GetByIdWithRoleClaims(Guid id)
        {
            var userProfile = await _userProfileService.GetByIdWithClaims(id);

            return CustomResponse(_mapper.Map<UserProfileViewModel>(userProfile));
        }

        [ClaimsAuthorize(nameof(UserProfile), "list")]
        [HttpGet("get-paged-list")]
        public async Task<ActionResult<IPagedList<UserProfileViewModel>>> GetPagedList([FromQuery] FilteredPagedListParameters parameters)
        {
            var pagedList = await _userProfileService.GetPagedList(parameters);

            return CustomResponse(_mapper.Map<IPagedList<UserProfileViewModel>>(pagedList));
        }

    }
}