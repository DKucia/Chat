using Chat.Api.Settings;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Api.Services.Auth
{
    public class JwtHandler : IJwtHandler
    {
        public string GetJwt(IEnumerable<Claim> claims,string secret,DateTime expired)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secret));
            var algorithm = SecurityAlgorithms.HmacSha256;
            var signingCredentials = new SigningCredentials(key, algorithm);
            var jwt = new JwtSecurityToken(
                    expires:expired,
                    claims: claims,
                    signingCredentials: signingCredentials
                );

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }
    }
}
