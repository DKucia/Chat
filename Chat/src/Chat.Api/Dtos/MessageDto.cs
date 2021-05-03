using System;

namespace Chat.Api.Dtos
{
    public class MessageDto
    {
        public string Id { get; set; }
        public string ConversationId { get; set; }
        public string Content { get; set; }
        public UserInfo UserInfo { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}