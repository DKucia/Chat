using Chat.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Hubs
{
    public interface IChatHub
    {
        Task ReceiveMessage(MessageDto messageDto);
        Task NewConversation(ConversationDto conversationDto);
    }
}
