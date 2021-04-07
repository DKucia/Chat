using Chat.Api.Dtos.Auth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services.Auth
{
    public interface IAuthService
    {
        Task<JwtToken> Login(LoginDto loginDto);
        Task Register(RegisterDto registerDto);
    }
}
