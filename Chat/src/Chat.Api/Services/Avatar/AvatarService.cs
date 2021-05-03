using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services.Avatar
{
    public class AvatarService : IAvatarService
    {
        private const string _defaultAvatarPath = "/images/avatars/default.png";
        private const string _avatarPathDirectory = "/images/avatars/";
        private readonly IWebHostEnvironment _environment;

        public AvatarService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public string GetUserAvatarOrDefault(string username)
        {
            var path = Path.Combine(_environment.WebRootPath,"images","avatars" ,username);
            
            if (File.Exists(path+".png"))
            {
                return "https://localhost:44310"+$"{_avatarPathDirectory}{username}.png";
            }

            return "https://localhost:44310"+_defaultAvatarPath;
        }

        public async Task<string> SaveAvatar(IFormFile file,string username)
        {
            var path = Path.Combine(_environment.WebRootPath, "images", "avatars",username+".png");
            using(var fileStream=new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
            return GetUserAvatarOrDefault(username);
        }
    }
}
