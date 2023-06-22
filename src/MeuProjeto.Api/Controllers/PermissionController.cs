using System.Threading.Tasks;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/permissions")]
    public class PermissionController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IPermissionService _permissionService;
        private readonly IUser _user;

        public PermissionController(INotifier notificador,
                              IMapper mapper,
                              IPermissionService permissionService,
                              IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _permissionService = permissionService;
            _user = user;
        }

        [HttpGet("get-list")]
        public async Task<ActionResult<List<PermissionViewModel>>> GetList()
        {
            return CustomResponse(_mapper.Map<List<PermissionViewModel>>(await _permissionService.GetAll()));
        }
    }
}