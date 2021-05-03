using Chat.Api.Services.Avatar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AvatarController : ControllerBase
    {
        private readonly IAvatarService _avatarService;

        public AvatarController(IAvatarService avatarService)
        {
            _avatarService = avatarService;
        }

        [HttpGet("")]
        public IActionResult GetCurrentAvatar()
        {
            return Ok(_avatarService.GetUserAvatarOrDefault(HttpContext.User.Identity.Name));
        }

        [HttpPost("")]
        public async Task<IActionResult> SetNewAvatar(IFormFile file )
        {
            if (file.Length == 0)
            {
                throw new ArgumentException("Empty file");
            }
            var avatarPath=await _avatarService.SaveAvatar(file, HttpContext.User.Identity.Name);
            return Ok(avatarPath);
        }
    }
}
