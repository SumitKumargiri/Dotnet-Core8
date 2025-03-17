using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetMessageByReceivername
{
    public class GetMessageByReceivernameRequest : IRequest<List<ChatMessageinfo>>
    {
        public string SenderName {  get; set; }
        public string ReceiverName { get; set; }
        public Guid? ConnectionId { get; set; }
    }
}
