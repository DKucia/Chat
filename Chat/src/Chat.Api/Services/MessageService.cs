using Chat.Api.Data;
using Chat.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Driver.Linq;
using MongoDB.Driver;

namespace Chat.Api.Services
{
    public class MessageService : IMessageService
    {
        private readonly IMongoProvider<Message> _mongo;

        public MessageService(IMongoProvider<Message> mongo)
        {
            _mongo = mongo;
        }

        public async  Task<List<Message>> GetMessages(string conversationId, int number = 0, int size = 10)
        {
            return await _mongo.Querable.Where(c => c.ConversationId.Equals(conversationId)).OrderBy(c => c.CreatedOn).Skip(number * size).Take(size).ToListAsync();
        }

        public async Task InsertMessage(Message message)
        {
            await _mongo.Collection.InsertOneAsync(message);
        }
    }
}
