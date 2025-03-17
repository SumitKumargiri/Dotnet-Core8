using App.EnglishBuddy.Application.Repositories;
using App.EnglishBuddy.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.EnglishBuddy.Application.Features.UserFeatures.ChatMessage.GetChatMessages
{
    public class GetChatMessagesHandler : IRequestHandler<GetChatMessagesRequest, List<ChatMessageinfo>>
    {
        private readonly IChatMessageRepository _chatMessageRepository;

        public GetChatMessagesHandler(IChatMessageRepository chatMessageRepository)
        {
            _chatMessageRepository = chatMessageRepository;
        }

        public async Task<List<ChatMessageinfo>> Handle(GetChatMessagesRequest request, CancellationToken cancellationToken)
        {
            return await _chatMessageRepository.GetMessagesAsync(request.SenderId, request.ReceiverId, request.ConnectionId, cancellationToken);
        }
    }
}
