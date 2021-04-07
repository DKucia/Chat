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
    public class ChatHub : Hub
    {
        private static ConcurrentDictionary<string, string> _connectedUsers = new ConcurrentDictionary<string, string>();
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
    }
}
