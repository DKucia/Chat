using Chat.Api.Domain;
using Chat.Api.Dtos;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public static class Extensions
    {
        public static string ToBase64string(this byte[] value) => Convert.ToBase64String(value);
        public static byte[] FromBase64ToBytes(this string base64) => Convert.FromBase64String(base64);
        public static MessageDto AsDto(this Message message) => new MessageDto()
        {
            Id=message.Id,
            Content = message.Content,
            CreatedOn = message.CreatedOn,
            Username = message.Username
        };

        public static ConversationDto AsDto(this Conversation conversation) => new ConversationDto()
        {
            CreatedOn = conversation.CreatedOn,
            Id = conversation.Id,
            Name = conversation.Name
        };

        public static string UserId(this HttpContext context) => context.User.Claims.FirstOrDefault(c=>c.Type.Equals(System.Security.Claims.ClaimTypes.NameIdentifier))?.Value;
    }
}
