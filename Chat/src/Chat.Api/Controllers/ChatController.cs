using Chat.Api.Domain;
using Chat.Api.Dtos;
using Chat.Api.Hubs;
using Chat.Api.Services;
using Chat.Api.Services.Avatar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
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
        private readonly IUserService _userService;
        private readonly IHubContext<ChatHub, IChatHub> _hubContext;

        public ChatController(IConversationService conversationService,IMessageService messageService
            ,IAvatarService avatarService,IUserService userService,IHubContext<ChatHub,IChatHub> hubContext)
        {
            _conversationService = conversationService;
            _messageService = messageService;
            _avatarService = avatarService;
            _userService = userService;
            _hubContext = hubContext;
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
            var isExist = await _userService.IsExist(dto.Username);
            if (!isExist) throw new Exception($"User with username= {dto.Username} don't exist");
            var conversation = new Conversation()
            {
                CreatedOn = DateTime.Now,
                Name = dto.Name,
                MemberUsernames= new List<string>() { dto.Username,HttpContext.User.Identity.Name}
            };
            await _conversationService.InsertConversation(conversation);
            var connectionId = ChatHub.GetConntetionId(dto.Username);
            if (connectionId != null)
            {
                await _hubContext.Clients.Client(connectionId).NewConversation(conversation.AsDto());
            }
            return Ok(conversation.AsDto());
        }
    }
}
