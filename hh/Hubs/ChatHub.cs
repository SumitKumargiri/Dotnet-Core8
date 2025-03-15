using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.EnglishBuddy.API.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatMessageRepository _chatMessageRepository;
        public ChatHub(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<bool> SendMessage(Guid senderId, Guid receiverId, string message, Guid connectionId, string sendername, CancellationToken cancellationToken)
        {
            try
            {
                var chatMessage = new ChatMessageinfo
                {
                    SenderName = sendername,
                    SenderId = senderId,
                    ReceiverId = receiverId,
                    Message = message,
                    ConnectionId = connectionId
                };

                await _chatMessageRepository.AddAsync(chatMessage, cancellationToken);
                await Clients.Client(connectionId.ToString()).SendAsync("ReceiveMessage", senderId.ToString(), message);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
