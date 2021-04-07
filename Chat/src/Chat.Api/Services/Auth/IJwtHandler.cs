using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace Chat.Api.Services.Auth
{
    public interface IJwtHandler
    {
        string GetJwt(IEnumerable<Claim> claims, string secret,DateTime expired);
    }
}