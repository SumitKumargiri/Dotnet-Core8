using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessages
{
    public class GetChatMessagesRequest : IRequest<List<ChatMessageinfo>>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public Guid ConnectionId { get; set; }
    }
}
