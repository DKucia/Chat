﻿using Chat.Api.Domain;
using Chat.Api.Services;
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

        static ChatHub()
        {
            _connectedUsers = new ConcurrentDictionary<string, string>();
        }

        public ChatHub(IMessageService messageService,IConversationService conversationService)
        {
            _messageService = messageService;
            _conversationService = conversationService;
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
            var connetions = GetConnectonsIds(conversation.UserIds);
            await Clients.Clients(connetions).ReceiveMessage(message.AsDto());
        }

        public override Task OnConnectedAsync()
        {
            _connectedUsers.TryAdd(this.Context.UserIdentifier, this.Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            _connectedUsers.TryRemove(Context.UserIdentifier, out var g);
            return base.OnDisconnectedAsync(exception);
        }

        private List<string> GetConnectonsIds(List<string> userIds)
        {
            var results = new List<string>();
            userIds.ForEach(c =>
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
