using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.CreateChatMessages
{
    public class ChatMessageRequest : IRequest<ChatMessageResponse>
    {
        public Guid Id { get; set; }
        public string SenderName {  get; set; }
        public string ReceiverName {  get; set; }
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
        public string Message { get; set; }
        public Guid ConnectionId { get; set; }
        public DateTime Time { get; set; }
        public DateTime? CreatedDate { get; set; }
        public Guid? Createdby { get; set; }
        public DateTime? UpdateDate { get; set; }
        public Guid? Updatedby { get; set; }
        public bool IsActive { get; set; }

    }
}
