using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services.Avatar
{
    public interface IAvatarService
    {
        string GetUserAvatarOrDefault(string username);
        Task<string> SaveAvatar(IFormFile file, string username);
    }
}
