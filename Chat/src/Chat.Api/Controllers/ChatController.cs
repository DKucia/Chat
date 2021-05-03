using Chat.Api.Domain;
using Chat.Api.Dtos;
using Chat.Api.Services;
using Chat.Api.Services.Avatar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IConversationService _conversationService;
        private readonly IMessageService _messageService;
        private readonly IAvatarService _avatarService;

        public ChatController(IConversationService conversationService,IMessageService messageService,IAvatarService avatarService)
        {
            _conversationService = conversationService;
            _messageService = messageService;
            _avatarService = avatarService;
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> Index()
        {

            var results = await _conversationService.GetConversations(HttpContext.User.Identity.Name);
            return Ok(results.Select(c=>c.AsDto()));
        }

        [HttpGet("messages/{conversationId}")]
        public async Task<IActionResult> GetMessages(string conversationId)
        {
            //todo
            var hasAccess = await _conversationService.HasAccessToConversation(HttpContext.User.Identity.Name, conversationId);
            var result = await _messageService.GetMessages(conversationId);
            return Ok(result.Select(c=>c.AsDto(_avatarService)));
        }

        [HttpPost("conversations")]
        public async Task<IActionResult> CreateConversation(CreateConversationDto dto)
        {
            var conversation = new Conversation()
            {
                CreatedOn = DateTime.Now,
                Name = dto.Name,
                MemberUsernames= new List<string>() { dto.Username,HttpContext.User.Identity.Name}
            };
            await _conversationService.InsertConversation(conversation);
            return Ok(conversation.AsDto());
        }
    }
}
