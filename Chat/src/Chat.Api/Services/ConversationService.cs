using Chat.Api.Data;
using Chat.Api.Domain;
using Chat.Api.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace Chat.Api.Services
{
    public class ConversationService : IConversationService
    {
        private readonly IMongoProvider<Conversation> _conversationRepostiory;

        public ConversationService(IMongoProvider<Conversation> conversationRepostiory)
        {
            _conversationRepostiory = conversationRepostiory;
        }

        public async Task<List<Conversation>> GetConversations(string userId, int size = 10)
        {
            return await _conversationRepostiory.Querable.Where(c => c.UserIds.Contains(userId)).Take(size).ToListAsync();
        }

        public async Task<Conversation> GetConversationById(string conversationId)
        {
            return await _conversationRepostiory.Querable.FirstOrDefaultAsync(c => c.Id.Equals(conversationId));
        }

        public async  Task InsertConversation(Conversation conversation)
        {
            await _conversationRepostiory.Collection.InsertOneAsync(conversation);
        }

        public async Task<bool> HasAccessToConversation(string userId, string conversationId)
        {
            return await _conversationRepostiory.Querable.AnyAsync(c => c.Id.Equals(conversationId) && c.UserIds.Contains(userId));
        }
    }
}
