using System.Threading.Tasks;
using MeuProjeto.Api.ViewModels;
using MeuProjeto.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using MeuProjeto.Business.DTOs;
using Microsoft.AspNetCore.Authorization;
using System;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Drawing;
using System.Collections.Generic;
using MeuProjeto.Api.Extensions;

namespace MeuProjeto.Api.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/users")]
    public class UserController : MainController
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;
        private readonly IUser _user;

        public UserController(INotifier notificador,
                              IMapper mapper,
                              IUserService userService,
                              IUser user) : base(notificador, user)
        {
            _mapper = mapper;
            _userService = userService;
            _user = user;
        }

        [ClaimsAuthorize(nameof(User), nameof(Create))]
        [HttpPost("create")]
        public async Task<ActionResult> Create(CreateUserViewModel createUser, IFormFile imageFile)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _mapper.Map<User>(createUser);

            string icon, image;
            ResizeImage(imageFile, out icon, out image);
            user.Icon = icon;
            user.Image = image;

            await _userService.Create(user, createUser.Password);

            return CustomResponse(createUser);
        }

        [ClaimsAuthorize(nameof(User), nameof(Update))]
        [HttpPut("update")]
        public async Task<ActionResult> Update(UpdateUserViewModel updateUser, IFormFile imageFile)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = _mapper.Map<UpdateUser>(await _userService.GetById(updateUser.Id));

            _mapper.Map<UpdateUserViewModel, UpdateUser>(updateUser, user);

            string icon, image;
            ResizeImage(imageFile, out icon, out image);
            user.Icon = icon;
            user.Image = image;

            await _userService.Update(user);

            return CustomResponse(updateUser);
        }

        [ClaimsAuthorize(nameof(User), nameof(Delete))]
        [HttpDelete("delete/{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            await _userService.Delete(id);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(User), nameof(Deactivate))]
        [HttpPatch("activate/{id}")]
        public async Task<ActionResult> Activate(Guid id)
        {
            await _userService.Activate(id);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(User), nameof(Deactivate))]
        [HttpPatch("deactivate/{id}")]
        public async Task<ActionResult> Deactivate(Guid id)
        {
            await _userService.Deactivate(id);

            return CustomResponse();
        }

        [HttpPut("change-user-password")]
        public async Task<ActionResult> ChangeUserPassword(ChangeUserPasswordViewModel changeUserPassword)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.ChangeUserPassword(changeUserPassword.Id, changeUserPassword.NewPassword);

            return CustomResponse();
        }

        [HttpPut("change-current-user-password")]
        public async Task<ActionResult> ChangeCurrentUserPassword(ChangeCurrentUserPasswordViewModel changeCurrentUserPassword)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.ChangeCurrentUserPassword(_user.GetUserId(), changeCurrentUserPassword.Password, changeCurrentUserPassword.NewPassword);

            return CustomResponse();
        }

        [ClaimsAuthorize(nameof(User), "list")]
        [HttpGet("{id}")]
        public async Task<ActionResult<UserViewModel>> GetById(Guid id)
        {
            var user = _mapper.Map<UserViewModel>(await _userService.GetById(id));
            return CustomResponse(user);
        }

        [ClaimsAuthorize(nameof(User), "list")]
        [HttpGet("get-by-id-for-update/{id}")]
        public async Task<ActionResult<UserForUpdateViewModel>> GetByIdForUpdate(Guid id)
        {
            var user = _mapper.Map<UserForUpdateViewModel>(await _userService.GetByIdForUpdate(id));
            return CustomResponse(user);
        }

        [ClaimsAuthorize(nameof(User), "list")]
        [HttpGet("get-by-id/{id}")]
        public async Task<ActionResult<UserViewModel>> GetByIdWithRoleClaims(Guid id)
        {
            var user = _mapper.Map<UserViewModel>(await _userService.GetByIdWithRoleClaims(id));
            return CustomResponse(user);
        }

        [HttpGet("get-by-id-current-user/{id}")]
        public async Task<ActionResult<UserCurrentUserViewModel>> GetByIdCurrentUser(Guid id)
        {
            var user = _mapper.Map<UserCurrentUserViewModel>(await _userService.GetByIdCurrentUser(id));
            return CustomResponse(user);
        }

        [ClaimsAuthorize(nameof(User), "list")]
        [HttpGet("get-paged-list")]
        public async Task<ActionResult<IPagedList<UserViewModel>>> GetPagedList([FromQuery] UserFilteredPagedListParameters parameters)
        {
            var pagedList = _mapper.Map<IPagedList<UserViewModel>>(await _userService.GetPagedList(parameters));
            return CustomResponse(pagedList);
        }

        [HttpGet("get-simple-paged-list")]
        public async Task<ActionResult<IPagedList<SimpleItemViewModel>>> GetSimplePagedList([FromQuery] UserFilteredPagedListParameters parameters)
        {
            var pagedList = _mapper.Map<IPagedList<SimpleItemViewModel>>(await _userService.GetSimplePagedList(parameters));
            return CustomResponse(pagedList);
        }

        private void ResizeImage(IFormFile file, out string icon, out string image)
        {
            string stringImagem = "";
            string stringImagemMenor = "";
            if (file != null)
            {
                byte[] byteImagem;

                byte[] byteImagemMenor;

                using (Stream inputStream = file.OpenReadStream())
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        inputStream.CopyTo(ms);

                        using (Image img = Image.FromStream(ms))
                        {
                            int newHeight = 100;
                            int newWidth = 100;

                            using (Bitmap b = new Bitmap(img, new Size(newWidth, newHeight)))
                            {
                                using (MemoryStream ms2 = new MemoryStream())
                                {
                                    b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byteImagemMenor = ms2.ToArray();

                                    stringImagemMenor = "data:image;base64, " + Convert.ToBase64String(byteImagemMenor);
                                }
                            }

                            newHeight = 1200;
                            newWidth = 1200;

                            using (Bitmap b = new Bitmap(img, new Size(newWidth, newHeight)))
                            {
                                using (MemoryStream ms2 = new MemoryStream())
                                {
                                    b.Save(ms2, System.Drawing.Imaging.ImageFormat.Jpeg);
                                    byteImagem = ms2.ToArray();

                                    stringImagem = "data:image;base64, " + Convert.ToBase64String(byteImagem);
                                }
                            }
                        }
                    }
                }
            }

            icon = stringImagemMenor;
            image = stringImagem;
        }

    }
}