using System.Threading.Tasks;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;
using MeuProjeto.Api.Extensions;
using MeuProjeto.Business.Models;
using MeuProjeto.Api.ViewModels;
using System.Collections.Generic;
using MeuProjeto.Api.Controllers;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/logs")]
    public class LogController : MainController
    {
        private readonly IUser _user;
        private readonly IMapper _mapper;
        private readonly ILogService _logService;

        public LogController(ILogService logService,
                             IMapper mapper,
                             INotifier notificador,
                             IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _logService = logService;
            _user = user;
        }

        [ClaimsAuthorize(nameof(Log), "view")]
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<LogViewModel>> GetById(Guid id)
        {
            var log = await _logService.GetById(id);
            var logViewModel = _mapper.Map<LogViewModel>(log);
            return CustomResponse(logViewModel);
        }

        [ClaimsAuthorize(nameof(Log), "list")]
        [HttpGet("get-paged-list-tables")]
        public ActionResult<List<string>> GetPagedListTables()
        {
            var tablesNames = _logService.GetPagedListTables();
            return CustomResponse(tablesNames);
        }

        [ClaimsAuthorize(nameof(Log), "list")]
        [HttpGet("get-paged-list")]
        public async Task<ActionResult<IPagedList<LogListViewModel>>> GetPagedList([FromQuery] FilteredPagedListParametersLog parameters)
        {
            var pagedList = await _logService.GetPagedList(parameters);
            var pagedListViewModel = _mapper.Map<IPagedList<LogListViewModel>>(pagedList);
            return CustomResponse(pagedListViewModel);
        }
    }
}