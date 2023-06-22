using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using System;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/about")]
    public class AboutController : MainController
    {
        public AboutController(INotifier notificador, IUser user) : base(notificador, user)
        {
        }


        [HttpGet("version")]
        public ActionResult<AboutViewModel> GetVersion()
        {
            return CustomResponse(CurrentVersion());
        }

        [HttpGet("time")]
        public ActionResult<AboutViewModel> GetTime()
        {
            return CustomResponse(new { Time = DateTime.Now, UTCTime = DateTime.UtcNow });
        }
    }
}