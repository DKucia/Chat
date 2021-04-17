using Chat.Api.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Services
{
    public interface IMessageService
    {
        Task InsertMessage(Message message);
        Task<List<Message>> GetMessages(string conversationId, int number = 0, int size = 10);
    }
}
