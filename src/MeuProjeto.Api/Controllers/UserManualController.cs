using System.Threading.Tasks;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;
using MeuProjeto.Business.Models;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/user-manuals")]
    public class UserManualController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserManualService _userManualService;
        private readonly IUser _user;

        public UserManualController(INotifier notificador,
                              IMapper mapper,
                              IUserManualService userManualService,
                              IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _userManualService = userManualService;
            _user = user;
        }

        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateUserManualViewModel userManualViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var userManual = await _userManualService.GetById(userManualViewModel.Id);

            _mapper.Map<UpdateUserManualViewModel, UserManual>(userManualViewModel, userManual);

            userManual.LastReviewerId = UserId;

            await _userManualService.Update(userManual);

            return CustomResponse(userManualViewModel);
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<UserManualViewModel>> GetById(Guid id)
        {
            var UserManual = _mapper.Map<UserManualViewModel>(await _userManualService.GetById(id));
            return CustomResponse(UserManual);
        }

        [HttpGet("get-by-code/{code}")]
        public async Task<ActionResult<UserManualViewModel>> GetByCode(string code)
        {
            var UserManual = _mapper.Map<UserManualViewModel>(await _userManualService.GetByCode(code));
            return CustomResponse(UserManual);
        }

        [HttpGet("get-paged-list")]
        public async Task<ActionResult<IPagedList<UserManualListViewModel>>> GetPagedList([FromQuery] FilteredPagedListParameters parameters)
        {
            return CustomResponse(_mapper.Map<IPagedList<UserManualListViewModel>>(await _userManualService.GetPagedList(parameters)));
        }
    }
}