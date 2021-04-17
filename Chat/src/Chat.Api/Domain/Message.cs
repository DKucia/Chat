using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chat.Api.Domain
{
    public class Message
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
