using Chat.Api.Domain;
using Chat.Api.Dtos;
using Chat.Api.Services;
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

        public ChatController(IConversationService conversationService,IMessageService messageService)
        {
            _conversationService = conversationService;
            _messageService = messageService;
        }

        [HttpGet("conversations")]
        public async Task<IActionResult> Index()
        {
            var id = HttpContext.UserId();
            var results = await _conversationService.GetConversations(id);
            return Ok(results);
        }

        [HttpGet("messages/{conversationId}")]
        public async Task<IActionResult> GetMessages(string conversationId)
        {
            //todo
            var hasAccess = await _conversationService.HasAccessToConversation(HttpContext.UserId(), conversationId);
            var result = await _messageService.GetMessages(conversationId);
            return Ok(result);
        }

        [HttpPost("conversations")]
        public async Task<IActionResult> CreateConversation(CreateConversationDto dto)
        {
            var conversation = new Conversation()
            {
                CreatedOn = DateTime.Now,
                Name = dto.Name,
                UserIds = dto.Usernames
            };
            await _conversationService.InsertConversation(conversation);
            return Ok(conversation.AsDto());
        }
    }
}
