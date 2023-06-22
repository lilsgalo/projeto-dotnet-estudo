using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System;
using MeuProjeto.Business.Interfaces;
using HealthChecks.UI.Configuration;
using MeuProjeto.Api.Extensions;
using MeuProjeto.Business.DTOs;
using MeuProjeto.Api.ViewModels;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/settings")]
    public class SettingsController : MainController
    {
        private readonly IMapper _mapper;
        private readonly ISettingsService _settingsService;
        private readonly IUser _user;

        public SettingsController(INotifier notificador,
                              IMapper mapper,
                              ISettingsService settingsService,
                              IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _settingsService = settingsService;
            _user = user;
        }

        [ClaimsAuthorize(nameof(Settings), "view")]
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<SettingsViewModel>> GetById(Guid id)
        {
            var settings = await _settingsService.GetById(id);

            var viewModel =_mapper.Map<SettingsViewModel>(settings);

            return CustomResponse(viewModel);
        }

        [ClaimsAuthorize(nameof(Settings), "update")]
        [HttpPut("update")]
        public async Task<ActionResult> Update(SettingsUpdateViewModel viewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var settings = await _settingsService.GetById(viewModel.Id);

            _mapper.Map(viewModel, settings);

            await _settingsService.Update(settings);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(Settings), "list")]
        [HttpGet("get-paged-list")]
        public async Task<ActionResult<IPagedList<SettingsViewModel>>> GetPagedList([FromQuery] FilteredPagedListParameters parameters)
        {
            var settings = await _settingsService.GetPagedList(parameters);

            var settingsViewModel = _mapper.Map<IPagedList<SettingsViewModel>>(settings);

            return CustomResponse(settingsViewModel);
        }
    }
}