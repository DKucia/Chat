using Chat.Api.Domain;
using Chat.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public interface IConversationService
    {
        Task<List<Conversation>> GetConversations(string userId, int size = 10);
        Task<Conversation> GetConversationById(string conversationId);
        Task InsertConversation(Conversation conversation);
        Task<bool> HasAccessToConversation(string userId, string conversationId);
    }
}
