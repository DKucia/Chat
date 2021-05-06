using Chat.Api.Domain;
using Chat.Api.Services;
using Chat.Api.Services.Avatar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Hubs
{
    [Authorize]
    public class ChatHub : Hub<IChatHub>
    {
        private static ConcurrentDictionary<string, string> _connectedUsers;
        private readonly IMessageService _messageService;
        private readonly IConversationService _conversationService;
        private readonly IAvatarService _avatarService;

        static ChatHub()
        {
            _connectedUsers = new ConcurrentDictionary<string, string>();
        }

        public ChatHub(IMessageService messageService,IConversationService conversationService,IAvatarService avatarService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
            _avatarService = avatarService;
        }

        public async Task SendMessage(string conversationId,string content)
        {
            var conversation = await _conversationService.GetConversationById(conversationId);
            if (conversation is null) return;

            var message = new Message()
            {
                ConversationId=conversationId,
                Content = content,
                CreatedOn = DateTime.Now,
                Username=Context.User.Identity.Name
            };
            await _messageService.InsertMessage(message);
            var connetions = GetConnectonsIds(conversation.MemberUsernames);
            await Clients.Clients(connetions).ReceiveMessage(message.AsDto(_avatarService));
        }

        public override Task OnConnectedAsync()
        {
            _connectedUsers.TryAdd(this.Context.User.Identity.Name, this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers.TryRemove(this.Context.User.Identity.Name, out var _);
            return base.OnDisconnectedAsync(exception);
        }
        public static string GetConntetionId(string username)
        {
            if (_connectedUsers.TryGetValue(username, out var val))
            {
                return val;
            }
            return null; 
        }

        private List<string> GetConnectonsIds(List<string> usernames)
        {
            var results = new List<string>();
            usernames.ForEach(c =>
            {
                if (_connectedUsers.TryGetValue(c, out var val))
                {
                    results.Add(val);
                }
            });
            return results;
        }
    }
}
