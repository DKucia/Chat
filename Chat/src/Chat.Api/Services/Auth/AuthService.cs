using Chat.Api.Data;
using Chat.Api.Domain;
using Chat.Api.Dtos.Auth;
using Chat.Api.Exceptions;
using Chat.Api.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Chat.Api.Services.Auth
{
    public class AuthService : IAuthService
    {
        private readonly IMongoProvider<User> _mongoProvider;
        private readonly IEncrypter _encrypter;
        private readonly IJwtHandler _jwtHandler;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IMongoProvider<User> mongoProvider,IEncrypter encrypter,IJwtHandler jwtHandler,IOptions<JwtSettings> jwtSettings)
        {
            _mongoProvider = mongoProvider;
            _encrypter = encrypter;
            _jwtHandler = jwtHandler;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<JwtToken> Login(LoginDto loginDto)
        {
            var user =await  _mongoProvider.Querable.FirstOrDefaultAsync(c => c.Email.Equals(loginDto.Email));
            if (user is null)
            {
                throw new InvalidCredentialsException();
            }
            var hash = _encrypter.GetHash(loginDto.Password, user.PasswordSalt.FromBase64ToBytes()).ToBase64string();
            if (!hash.Equals(user.PasswordHash)) throw new InvalidCredentialsException();

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.Id.ToString())
            };
            var token = new JwtToken()
            {
                Token = _jwtHandler.GetJwt(claims, _jwtSettings.Secret,DateTime.Now.AddMinutes(_jwtSettings.Expired))
            };
            return token;
        }

        public async  Task Register(RegisterDto dto)
        {
            var isExistByEmail = await _mongoProvider.Querable.AnyAsync(c => c.Email.Equals(dto.Email));
            if (isExistByEmail) return;
            var isExistByUsername = await _mongoProvider.Querable.AnyAsync(c => c.Username.Equals(dto.Username));
            if (isExistByUsername) return;
            var salt = _encrypter.GenerateRandomSalt();
            var user = new User()
            {
                Email = dto.Email,
                Username = dto.Username,
                PasswordHash =_encrypter.GetHash(dto.Password, salt).ToBase64string(),
                PasswordSalt = salt.ToBase64string()
            };
            await _mongoProvider.Collection.InsertOneAsync(user);
        }
    }
}
